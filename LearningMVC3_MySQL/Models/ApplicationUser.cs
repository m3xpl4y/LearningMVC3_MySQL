using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(60)]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nachname")]
        public string LastName { get; set; }

        [StringLength(10)]
        [Display(Name = "Geschlecht")]
        public string Gender { get; set; }
        [StringLength(60)]
        [Display(Name = "Stadt")]
        public string City { get; set; }
    }
}
