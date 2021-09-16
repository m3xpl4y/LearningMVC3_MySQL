using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.ViewModel
{
    public class CreateRoleViewModel
    {
        public string Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
