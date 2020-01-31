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

        [HttpPost]
        public IActionResult Menu(Matratt order)
        {
            return View();
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
            return View();
        }
        [HttpPost]
        public IActionResult AddNewDish(CreateDishViewModel model)
        {
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
