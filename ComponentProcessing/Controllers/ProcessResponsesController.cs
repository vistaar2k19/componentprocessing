using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComponentProcessing.Model;
using ComponentProcessingDbContext.Data;
using ComponentProcessing.Data;
using RestSharp;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.Extensions.Logging;

namespace ComponentProcessing.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProcessResponsesController : ControllerBase
    {
        private readonly ComponentProcessingContext _context;
        private readonly ILogger _logger;

        public ProcessResponsesController(ComponentProcessingContext context)
        {
            _context = context;
           
        }

        // GET: api/ProcessResponses
        [HttpPost("ProcessResponses")]
        public async Task<IActionResult> ProcessResponses([FromBody] ProcessRequest request, [FromHeader] string Authorization)
        {
            try
            {
                //_logger.LogInformation("Validating data");
                ProcessResponse pr = new ProcessResponse();
                if (request.Name == "" || request.Quantity == 0 || request.Quantity <= 0 || request.ContactNumber == "" || request.ComponentType == "" || request.ComponentName == "")
                {
                    return StatusCode(400, new { message = "Insufficient or empty details" });
                }

                if (request.ComponentType == "Integral")
                {
                    Repair r = new Repair();
                    pr.ProcessingCharge = r.ProcessingCharge;
                    pr.DateOfDelivery = DateTime.Today.AddDays(r.ProcessingDuration);
                    //_logger.LogInformation("Integral type");

                }
                else if (request.ComponentType == "Accessory")
                {
                    Replace r = new Replace();
                    pr.ProcessingCharge = r.ProcessingCharge;
                    pr.DateOfDelivery = DateTime.Today.AddDays(r.ProcessingDuration);
                    //_logger.LogInformation("Accessory Type");

                }
                else return StatusCode(400, new { message = "Either Integral or accessory value allowed for component type" }); ;

                //_logger.LogInformation("Calling PackagingDeliveryCharge microservice for packaging and delivery charge");
                //microserviceCall
                var client = new RestClient("http://20.237.80.83/GetPackagingDeliveryCharge");
                client.AddDefaultHeader("Authorization", Authorization);
                RestRequest restRequest = new RestRequest($"{request.ComponentType}/{request.Quantity}", Method.Get);
                var response = await client.ExecuteAsync(restRequest);

                //_logger.LogInformation("Microservice call complete");

                pr.PackagingandDeliveryCharge = Convert.ToInt32(response.Content);
                if(pr.PackagingandDeliveryCharge <= 0)
                {
                    //_logger.LogInformation("Error occured while calculating Packaging and delivery charge");
                    return StatusCode(500, new { message = "Error occured while calculating Packaging and delivery charge" });
                }


                if (_context.ProcessResponse.Any())
                {
                    var requestId = _context.ProcessRequest.OrderByDescending(s => s.Id).FirstOrDefault().Id;
                    pr.RequestId = (int)requestId;
                    if (pr.RequestId == 0)
                    {
                        pr.RequestId = 1;
                    }
                    else pr.RequestId++;
                }
                else pr.RequestId = 1;

                var temp = _context.ProcessRequest.Add(request);
                await _context.SaveChangesAsync();

                //_logger.LogInformation("Request data stored in datbase");

                return Ok(pr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //_logger.LogInformation("Exception occured : " + e.Message);
                return StatusCode(500, new { message = "Error occured" });

            }


        }



        // POST: api/ProcessResponses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("CompleteProcessing")]
        public IActionResult CompleteProcessing([FromBody] ProcessResponse processResponse)
        {
            try
            {

                if (processResponse.RequestId <= 0 || processResponse.ProcessingCharge <= 0 || processResponse.PackagingandDeliveryCharge <= 0 )
                {
                    //_logger.LogInformation("Insufficient or empty details");
                    return StatusCode(400, new { message = "Insufficient or empty details" });
                }

                _context.ProcessResponse.Add(processResponse);
                _context.SaveChanges();

                //_logger.LogInformation("Response data saved in database");

                return StatusCode(StatusCodes.Status201Created, new { message = "Success" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //_logger.LogInformation("Exception occured : "+ e.Message);
                return StatusCode(500, new { message = "Error occured" });

            }
        }

      

        //private bool ProcessRequestExists(int requestId)
        //{
        //    return _context.ProcessResponse.Any(e => e.RequestId == requestId);
        //}
    }
}
