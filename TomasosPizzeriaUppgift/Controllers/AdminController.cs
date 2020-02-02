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
            var model = Services.Services.Instance.GetOrders(1);
            return View(model);
        }

        public IActionResult OrderType(int id)
        {
            var model = Services.Services.Instance.GetOrders(id);
            return View("Orders",model);
        }
        public IActionResult OrderDetailView(int id)
        {
            var model = Services.Services.Instance.OrderDetailView(id);
            return View(model);
        }
        public IActionResult DeliverOrder(int id)
        {
            Services.Services.Instance.DeliverOrder(id);
            var model = Services.Services.Instance.OrderDetailView(id);
            return View("OrderDetailView",model);
        }
        public IActionResult DeleteOrder(int id)
        {
            Services.Services.Instance.DeleteOrder(id);

            return RedirectToAction("Orders");
        }

        [HttpGet]
        public IActionResult Menu()
        {
            var model = Services.Services.Instance.GetMenuInfo();
            return View(model);
        }

        
        public IActionResult DeleteDish(int id)
        {
            Services.Services.Instance.DeleteDish(id);
            var model = Services.Services.Instance.GetMenuInfo();
            return View("Menu", model);
        }
        [HttpGet]
        public IActionResult AddNewDish()
        {
            var model = Services.Services.Instance.GetMenuInfo();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddNewDish(MenuPage model)
        {
            
            model = Services.Services.Instance.CheckMatrattsValidation(model);
            var newModel = model.NewDish;
            if (model.NewDish.Matratt.MatrattNamn.Length > 1 && model.NewDish.SelectedListItem.Count != 0 && model.NewDish.Matratt.MatrattTyp != 0 && model.MatrattsnamnTaken == false)
            {
                Services.Services.Instance.CreateDish(model.NewDish);
                return RedirectToAction("Menu");
            }
            model = Services.Services.Instance.GetMenuInfo();
            model = Services.Services.Instance.SetValidtion(model, newModel);
            return View(model);
        }
        [HttpGet]
        public IActionResult AddIngrediens()
        {
            return RedirectToAction("Menu");
        }
        [HttpPost]
        public IActionResult AddIngrediens(int id)
        {
            return RedirectToAction("Menu");
        }
        [HttpGet]
        public IActionResult EditDish()
        {
            return View();
        }
        [HttpPost]
        public IActionResult EditDish(MenuPage model)
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
