using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Interface.Repository;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.Models.Identity;
using TomasosPizzeriaUppgift.Models.IdentityLogic;
using TomasosPizzeriaUppgift.Models.Repository;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Services
{
    public class ServiceAccount
    {
        private static ServiceAccount instance = null;
        private static readonly Object padlock = new Object();
        private IRepository _repository;
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;



        public static ServiceAccount Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceAccount();
                        instance._repository = new DBRepository();
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

                    }
                    return instance;

                }
            }
        }

        public ServiceAccount()
        {
        }
        public Kund CheckUserName(Kund customer)
        {
            return _repository.CheckUserName(customer);
        }
        public Kund GetInloggedCustomerInfo(HttpRequest Request)
        {
            var customerId = GetCustomerIDCache(Request);
            return _repository.GetById(customerId);
        }
        public int GetCustomerIDCache(HttpRequest Request)
        {
            return _cache.GetCustomerIDCache(Request);
        }

        public void SetCustomerCache(LoginViewModel model, HttpRequest request, HttpResponse response)
        {
            var customer = _repository.GetCustomerByUsername(model.Username);
            _cache.SetCustomerCache(customer, request, response);
        }
    }
}
