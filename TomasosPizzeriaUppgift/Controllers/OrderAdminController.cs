using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeriaUppgift.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class OrderAdminController : Controller
    {
        [HttpGet]
        public IActionResult Orders()
        {
            var model = OrderAdminService.Instance.GetOrders(1);
            return View(model);
        }

        public IActionResult OrderType(int id)
        {
            var model = OrderAdminService.Instance.GetOrders(id);
            return View("Orders", model);
        }
        public IActionResult OrderDetailView(int id)
        {
            var model = OrderAdminService.Instance.OrderDetailView(id);
            return View(model);
        }
        public IActionResult DeliverOrder(int id)
        {
            OrderAdminService.Instance.DeliverOrder(id);
            var model = OrderAdminService.Instance.OrderDetailView(id);
            return View("OrderDetailView", model);
        }
        public IActionResult DeleteOrder(int id)
        {
            OrderAdminService.Instance.DeleteOrder(id);

            return RedirectToAction("Orders");
        }
    }
}
