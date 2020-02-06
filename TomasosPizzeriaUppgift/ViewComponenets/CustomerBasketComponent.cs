using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Services;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.ViewComponenets
{
    public class CustomerBasketComponent : ViewComponent
    {
        public IViewComponentResult Invoke(MenuPage model)
        {
            //var model = MenuService.Instance.MenuPageData(HttpContext.Request,HttpContext.Response);
            return View("_Menu", model);
        }
        
    }
}
