using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rolemanager;

        public AdministrationController(RoleManager<IdentityRole> rolemanager)
        {
            this.rolemanager = rolemanager;
        }


        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
    }
}
