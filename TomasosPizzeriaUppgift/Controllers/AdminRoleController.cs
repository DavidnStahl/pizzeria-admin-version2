using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Users()
        {
            var model = Services.Services.Instance.GetAllUsers(userManager,roleManager);
            return View(model);
        }
        [HttpPost]
        public IActionResult Users(Kund customer)
        {
            return View();
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

        [HttpGet]
        public IActionResult EditRole(string userName)
        {
            var model = Services.Services.Instance.GetUserIdentityInfoByUsername(userName, userManager, roleManager).Result;
            return View(model);
        }
        [HttpPost]
        public IActionResult EditRole(UpdateRoleViewModel model)
        {
            var result = Services.Services.Instance.UpdateRole(roleManager, model);
            if(result == false)
            {
                return View(model);
            }
            return RedirectToAction("Users");
        }



    }
}
