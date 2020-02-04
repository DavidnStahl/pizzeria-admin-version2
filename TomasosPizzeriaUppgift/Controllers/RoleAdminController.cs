using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;
using TomasosPizzeriaUppgift.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class RoleAdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;


        public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
       
        [HttpGet]
        [Authorize]
        public IActionResult Users()
        {
            var model = RoleAdminService.Instance.GetAllUsers(roleManager,"All");
            return View(model);
        }
        public IActionResult DeleteUser(string username)
        {
            RoleAdminService.Instance.DeleteUser(username,Request,Response);           
            return RedirectToAction("Users");
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = RoleAdminService.Instance.IdentityCreateRole(roleManager, model);
                if (result == true)
                {
                    return RedirectToAction("userRole");
                }
            }
            return View();
        }

        public IActionResult ChangeRoleTypeUser(string changeRoleTo, string id)
        {
            RoleAdminService.Instance.ChangeRoleTypeUser(changeRoleTo,id, userManager,roleManager);
            return RedirectToAction("Users");
        }
        public IActionResult RoleTypeSearch(string roleNameToSearch)
        {
            var model = RoleAdminService.Instance.GetAllUsers(roleManager, roleNameToSearch);
            return View("Users",model);
        }

    }
}
