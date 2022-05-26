using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ComponentProcessing.Model
{
    
    public class ProcessRequest
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please provide Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide Contact Number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Please provide Component type")]
        public string ComponentType { get; set; }
        [Required(ErrorMessage = "Please provide Component Name")]
        public string ComponentName { get; set; }
        [Required(ErrorMessage = "Please provide Quantity")]
        public int Quantity { get; set; }
    }
}
