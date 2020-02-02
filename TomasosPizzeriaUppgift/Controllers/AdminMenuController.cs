﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class AdminMenuController : Controller
    {
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddIngrediens(Produkt produkt)
        {
            var result = Services.Services.Instance.AddIngrediens(produkt);
            if (result == true)
            {

                return RedirectToAction("Menu");
            }
            ViewBag.Message = "true";
            return View();
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
    }
}