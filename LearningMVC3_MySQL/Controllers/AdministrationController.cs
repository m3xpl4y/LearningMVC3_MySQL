using LearningMVC3_MySQL.ViewModel;
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

        //Roles View
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        //Adding Roles
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await rolemanager.CreateAsync(identityRole);

                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "administration");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        //List of all Available Roles
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = rolemanager.Roles;
            return View(roles);
        }
    }
}
