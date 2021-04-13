using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LAB4_Daneil_Elias_Diego_Ramirez.Models.Data
{
    public class Task
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Project { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public string Date { get; set; }

    }
}
