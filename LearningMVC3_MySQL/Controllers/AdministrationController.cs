using LearningMVC3_MySQL.Models;
using LearningMVC3_MySQL.ViewModel;
using Microsoft.AspNetCore.Authorization;
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

        public UserManager<ApplicationUser> UserManager { get; }

        public AdministrationController(RoleManager<IdentityRole> rolemanager, UserManager<ApplicationUser> userManager)
        {
            this.rolemanager = rolemanager;
            UserManager = userManager;
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
        [Authorize]
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = rolemanager.Roles;
            return View(roles);
        }

        public async Task<IActionResult> Editrole(string id)
        {
            var role = await rolemanager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {id} cannot be found";
                return View("Not Found");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach (var user in UserManager.Users)
            {
                if(await UserManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }            

            return View(model);
        }
    }
}
