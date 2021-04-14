using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.Data
{
    public class Developer
    {
        [Required]
        public string User { get; set; }

        [Required]
        public string Password { get; set;}


    }
}
