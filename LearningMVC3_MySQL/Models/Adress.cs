using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Models
{
    public class Adress
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Straßennamen")]
        public string Street { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Stadt")]
        public string City { get; set; }
    }
}
