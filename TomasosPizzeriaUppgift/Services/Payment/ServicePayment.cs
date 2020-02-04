using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.Models.Identity;
using TomasosPizzeriaUppgift.Models.IdentityLogic;
using TomasosPizzeriaUppgift.Models.Repository;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Services
{
    public class ServicePayment
    {
        private static ServicePayment instance = null;
        private static readonly Object padlock = new Object();
        private IRepository _repository;
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;



        public static ServicePayment Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServicePayment();
                        instance._repository = new DBRepository();
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

                    }
                    return instance;

                }
            }
        }

        public ServicePayment()
        {
        }
        public void PayUser(HttpRequest request, HttpResponse response)
        {
            var userid = ServiceAccount.Instance.GetCustomerIDCache(request);
            var matratteradded = _cache.PayUser(request, response);
            UserPay(matratteradded, userid);
            _cache.DeleteFoodListCache(request, response);
        }
        public MenuPage PayPage(HttpRequest request, HttpResponse response)
        {
            var matratteradded = _cache.GetMatratterToPay(request, response);
            var model = new MenuPage();
            model.Matratteradded = matratteradded;
            model.mattratttyper = ServiceMenu.Instance.GetMatratttyper();
            return model;
        }
        public void UserPay(List<Matratt> matratter, int userid)
        {
            _repository.SaveOrder(matratter, userid);

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
