using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Models;

using TomasosPizzeriaUppgift.Models.IdentityLogic;
using TomasosPizzeriaUppgift.Models.Repository;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Services
{
    public class PaymentService
    {
        private static PaymentService instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryMenu _repository;
        private ICache _cache;

        public static PaymentService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new PaymentService();
                        instance._repository = new DBRepositoryMenu();
                        instance._cache = new CacheLogic();  
                    }
                    return instance;

                }
            }
        }

        public PaymentService()
        {
        }
        public void PayUser(HttpRequest request, HttpResponse response, System.Security.Claims.ClaimsPrincipal user)
        {
            var userid = _cache.GetCustomerIDCache(request);
            var matratteradded = _cache.PayUser(request, response);
            UserPay(matratteradded, userid,user);
            _cache.DeleteFoodListCache(request, response);
        }
        public MenuPage PayPage(HttpRequest request, HttpResponse response)
        {
            var matratteradded = _cache.GetMatratterToPay(request, response);
            var model = new MenuPage();
            model.Matratteradded = matratteradded;
            model.mattratttyper = MenuService.Instance.GetMatratttyper();
            return model;
        }
        public void UserPay(List<Matratt> matratter, int userid, System.Security.Claims.ClaimsPrincipal user)
        {
            _repository.SaveOrder(matratter, userid,user);

        }
        public bool CheckValidLogin(LoginViewModel model)
        {
            if (model == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
