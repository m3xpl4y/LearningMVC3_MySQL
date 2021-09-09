using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Models
{
    public class Person
    {

        public int Id { get; set; }

        public int AdressId { get; set; }
        public int PetId { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nachname")]
        public string LastName { get; set; }
        public Adress Adress { get; set; }
        public Pet Pet { get; set; }
    }
}
