using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Haustierart")]
        public string Type { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Tiername")]
        public string Name { get; set; }
    }
}
