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
            return _repository.GetAll().FirstOrDefault(r => r.AnvandarNamn == customer.AnvandarNamn);
        }
        public Kund GetInloggedCustomerInfo(HttpRequest Request)
        {
            var customerId = _cache.GetCustomerIDCache(Request);
            return _repository.GetAll().FirstOrDefault( r => r.KundId == customerId);   
        }
        public void SetCustomerCache(LoginViewModel model, HttpRequest request, HttpResponse response)
        {
            var customer = _repository.GetAll().FirstOrDefault(r => r.AnvandarNamn == model.Username);
            _cache.SetCustomerCache(customer, request, response);
        }
        public Kund CheckUserByUsernameAndPassword(Kund customer)
        {
            return _repository.GetAll().FirstOrDefault(r => r.AnvandarNamn == customer.AnvandarNamn && r.Losenord == customer.Losenord);
        }
        public Kund GetById(int id)
        {
            return _repository.GetAll().FirstOrDefault(r => r.KundId == id);
        }
        public bool CheckUserNameIsValid(Kund user, HttpRequest request)
        {
            var customer = CheckUserName(user);
            var customerid = _cache.GetCustomerIDCache(request);
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
            _repository.Create(user);
        }
        public void UpdateUser(Kund user, HttpRequest request)
        {
            user.KundId = _cache.GetCustomerIDCache(request);
            var customer = _repository.GetAll().FirstOrDefault(r => r.KundId == user.KundId);
            customer.Namn = user.Namn;
            customer.Gatuadress = user.Gatuadress;
            customer.Postnr = user.Postnr;
            customer.Postort = user.Postort;
            customer.Email = user.Email;
            customer.Telefon = user.Telefon;
            customer.AnvandarNamn = user.AnvandarNamn;
            customer.Losenord = user.Losenord;
            _repository.Update(user);
        }
        
    }
}
