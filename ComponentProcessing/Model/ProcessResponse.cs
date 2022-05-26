using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.Model
{
    public class ProcessResponse
    {
        [Key]
        [Required(ErrorMessage = "Please provide Request ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestId { get; set; }

        [Required(ErrorMessage = "Please provide Processing charge")]
        public int ProcessingCharge { get; set; }

        [Required(ErrorMessage = "Please provide Packaging and delivery charge")]
        public int PackagingandDeliveryCharge { get; set; }

        [Required(ErrorMessage = "Please provide Date of delivery")]
        [Column(TypeName = "date")]
        public DateTime DateOfDelivery { get; set; }

    }
}
