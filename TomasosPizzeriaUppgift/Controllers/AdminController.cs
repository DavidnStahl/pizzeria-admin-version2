﻿using System;
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
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AdminController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Orders()
        {
            var model = Services.Services.Instance.GetOrders("All");
            return View(model);
        }
        [HttpPost]
        public IActionResult Orders(string ordertyp)
        {
            var model = Services.Services.Instance.GetOrders(ordertyp);
            return View(model);
        }
        [HttpPost]
        public IActionResult OrderDetailView(int id)
        {
            var model = Services.Services.Instance.OrderDetailView(id);
            return View(model);
        }
        [HttpGet]
        public IActionResult Menu()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Menu(Matratt order)
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserRole(Kund customer)
        {
            return View();
        }
    }
}
