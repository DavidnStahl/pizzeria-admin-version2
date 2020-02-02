using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class AdminOrderController : Controller
    {
        [HttpGet]
        public IActionResult Orders()
        {
            var model = Services.Services.Instance.GetOrders(1);
            return View(model);
        }

        public IActionResult OrderType(int id)
        {
            var model = Services.Services.Instance.GetOrders(id);
            return View("Orders", model);
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
            return View("OrderDetailView", model);
        }
        public IActionResult DeleteOrder(int id)
        {
            Services.Services.Instance.DeleteOrder(id);

            return RedirectToAction("Orders");
        }
    }
}
