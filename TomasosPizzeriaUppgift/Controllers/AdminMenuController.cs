using System;
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
            var model = Services.ServiceMenu.Instance.GetMenuInfo();
            return View(model);
        }


        public IActionResult DeleteDish(int id)
        {
            Services.ServiceAdminDishes.Instance.DeleteDish(id);
            var model = Services.ServiceMenu.Instance.GetMenuInfo();
            return View("Menu", model);
        }
        [HttpGet]
        public IActionResult AddNewDish()
        {
            var model = Services.ServiceMenu.Instance.GetMenuInfo();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddNewDish(MenuPage model)
        {

            model = Services.ServiceAdminDishes.Instance.CheckMatrattsValidation(model);
            var newModel = model.NewDish;
            if (model.NewDish.Matratt.MatrattNamn.Length > 1 && model.NewDish.SelectedListItem.Count != 0 && model.NewDish.Matratt.MatrattTyp != 0 && model.MatrattsnamnTaken == false)
            {
                Services.ServiceAdminDishes.Instance.CreateDish(model.NewDish);
                return RedirectToAction("Menu");
            }
            model = Services.ServiceMenu.Instance.GetMenuInfo();
            model = Services.ServiceAdminDishes.Instance.SetValidtion(model, newModel);
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
            var result = Services.ServiceAdminDishes.Instance.AddIngrediens(produkt);
            if (result == true)
            {

                return RedirectToAction("Menu");
            }
            ViewBag.Message = "true";
            return View();
        }
        public IActionResult RemoveIngrediens(int id)
        {
            Services.ServiceAdminDishes.Instance.RemoveIngrediens(id);
            return RedirectToAction("Menu");
        }
        [HttpGet]
        public IActionResult EditDish(int id)
        {
            var model = Services.ServiceAdminDishes.Instance.GetDishToUpdate(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDish(UpdateDishViewModel model)
        {
            var oldmodel = Services.ServiceAdminDishes.Instance.GetDishToUpdate(model.id);
            if (ModelState.IsValid)
            {
                Services.ServiceAdminDishes.Instance.UpdateDish(model);
                return RedirectToAction("Menu");
            }
            return View(oldmodel);
        }
    }
}
