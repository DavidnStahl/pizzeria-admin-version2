using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TomasosPizzeriaUppgift.ViewModels;
using TomasosPizzeriaUppgift.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TomasosPizzeriaUppgift.Controllers
{
    public class PaymentController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        [Authorize]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            var result = PaymentService.Instance.CheckValidLogin(model);
            if (ModelState.IsValid && result == true)
            {
                return RedirectToAction("Pay");
                
            }

            ViewBag.Error = "Inloggning Misslyckades";
            return View(model);
        }

        [Authorize]
        public IActionResult Pay()
        {
            PaymentService.Instance.PayUser(Request, Response,User);
            return View();
        }
    }
}
