using LearningMVC3_MySQL.Data;
using LearningMVC3_MySQL.Models;
using LearningMVC3_MySQL.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LearningMVC3_MySQL.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> rolemanager;

        public UserManager<ApplicationUser> UserManager { get; }
        public ApplicationDbContext Context { get; }

        public AdministrationController(RoleManager<IdentityRole> rolemanager, 
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.rolemanager = rolemanager;
            this.UserManager = userManager;
            this.Context = context;
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
        [HttpGet]
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

            var users = await UserManager.GetUsersInRoleAsync(role.Name);
            foreach (var user in users)
            {
                model.Users.Add(user.UserName);
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Editrole(EditRoleViewModel viewModel)
        {
            var role = await rolemanager.FindByIdAsync(viewModel.Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {viewModel.Id} cannot be found";
                return View("Not Found");
            }
            else
            {
                role.Name = viewModel.RoleName;
                var result = await rolemanager.UpdateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(viewModel);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersinRole(string roleId)
        {
            ViewBag.roleId = roleId;
            
            var role = await rolemanager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("Not Found");
            }

            ViewBag.roleName = role.Name;

            var model = new List<UserRoleViewModel>();

            foreach (var user in UserManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    isSelected = await UserManager.IsInRoleAsync(user, role.Name)
                };
                model.Add(userRoleViewModel);
                var test = userRoleViewModel.isSelected;
                Debug.WriteLine(test);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await rolemanager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with ID = {roleId} cannot be found";
                return View("Not Found");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(model[i].UserId);
                IdentityResult identityResult = null;

                if(model[i].isSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    identityResult = await UserManager.AddToRoleAsync(user, role.Name);
                    Debug.WriteLine("added");
                }
                else if (!model[i].isSelected && await UserManager.IsInRoleAsync(user, role.Name))
                {
                    identityResult = await UserManager.RemoveFromRoleAsync(user, role.Name);
                    Debug.WriteLine("removed");
                }
                else
                {
                    continue;
                    //Because there are 2 more options what can be, if the user is selected and already in the role
                    //we do not what to do anything
                    //if the user is not selected and not in the role
                    //we do not what to do anything 
                    //so continue in code
                }
                if(identityResult.Succeeded)
                {
                    if (i < (model.Count) - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }
       
        public IActionResult DeleteRole(string id)
        {
            var role = Context.Roles.Where(r => r.Id == id).FirstOrDefault();
            Context.Roles.Remove(role);
            Context.SaveChanges();

            return RedirectToAction("ListRoles");
        }
    }
}
