using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeriaUppgift.Services;
using TomasosPizzeriaUppgift.ViewComponenets;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class MenuController : Controller
    {
        [Authorize]
        [HttpGet] 
        public IActionResult Menu()
        {
             var model = MenuService.Instance.MenuPageData(Request, Response);
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddItemCustomerBasket(int id)
        {
            var model = MenuService.Instance.CustomerBasket(id, Request, Response);
            //return PartialView("Menu", model);
            return ViewComponent("CustomerBasketComponent", model);
        }
        [Authorize]
        public ActionResult RemoveItemCustomerBasket(int id, int count)
        {
            var model = MenuService.Instance.RemoveItemCustomerBasket(id, count, Request, Response);
            return ViewComponent("CustomerBasketComponent", model);
        }
        public ActionResult LoadMenu()
        {
            var model = MenuService.Instance.MenuPageData(Request, Response);
            return ViewComponent("CustomerBasketComponent", model);
        }
    }
    
}
