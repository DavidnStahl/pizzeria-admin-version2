using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;


        public AdminRoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
       
        [HttpGet]
        [Authorize]
        public IActionResult Users()
        {
            var model = Services.Services.Instance.GetAllUsers(roleManager);
            return View(model);
        }
        public IActionResult DeleteUser(string username)
        {
            Services.Services.Instance.DeleteUser(username,Request,Response);           
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
                var result = Services.Services.Instance.IdentityCreateRole(roleManager, model);
                if (result == true)
                {
                    return RedirectToAction("userRole");
                }
            }
            return View();
        }

        public IActionResult ChangeRoleTypeUser(string changeRoleTo, string id)
        {
            Services.Services.Instance.ChangeRoleTypeUser(changeRoleTo,id, userManager,roleManager);
            return RedirectToAction("Users");
        }

    }
}
