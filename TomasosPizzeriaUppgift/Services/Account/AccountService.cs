using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class AccountService
    {
        private static AccountService instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryCustomers _repository;
        private ICache _cache;

        public static AccountService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AccountService();
                        instance._repository = new DBRepositoryCustomers();
                        instance._cache = new CacheLogic();
                    }
                    return instance;

                }
            }
        }

        public AccountService()
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
        public Kund GetUserId(Kund customer)
        {
            return _repository.GetUserId(customer);


        }
        public Kund GetById(int id)
        {
            return _repository.GetById(id);
        }
        public bool CheckUserNameIsValid(Kund user, HttpRequest request)
        {
            var customer = CheckUserName(user);
            var customerid = Instance.GetCustomerIDCache(request);
            var cachecustomer = GetById(customerid);
            if (customer == null)
            {
                return true;
            }
            else if (user.AnvandarNamn == cachecustomer.AnvandarNamn)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void SaveUser(Kund user)
        {
            _repository.SaveUser(user);
        }
        public void UpdateUser(Kund user, int userid, HttpRequest request, HttpResponse response)
        {
            _repository.UpdateUser(user, userid);
        }
        
    }
}
