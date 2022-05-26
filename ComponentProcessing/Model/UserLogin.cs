using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComponentProcessing.Model
{
    public class UserLogin
    {
        [Key]
        public string UserId { get; set; }
        public string UserPassword { get; set; }
    }
}
