using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;
using TomasosPizzeriaUppgift.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Models.Repository;
using TomasosPizzeriaUppgift.Models.IdentityLogic;
using Microsoft.AspNetCore.Identity;

namespace TomasosPizzeriaUppgift.Services
{
    public class MenuService
    {
        private static MenuService instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryMenu _repository;
        private IRepositoryCustomers _repositoryCustomer;
        private ICache _cache;


        public static MenuService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MenuService();
                        instance._repository = new DBRepositoryMenu();
                        instance._repositoryCustomer = new DBRepositoryCustomers();
                        instance._cache = new CacheLogic();
                    }
                    return instance;

                }
            }
        }

        public MenuService()
        {
        }
    
        public MenuPage GetMenuInfo()
        {
            return _repository.GetMenuInfo();
        }
        public Matratt GetMatratterById(int id)
        {
            return _repository.GetMatratterToCustomerbasket(id);
        }
        public List<MatrattTyp> GetMatratttyper()
        {
            return _repository.GetMatrattTyper();
        }
        public List<Matratt> GetMatratterCacheList(int id, string options, HttpRequest request, HttpResponse response)
        {

            return _cache.GetMatratterCacheList(id, options, request, response);
        }
        public MenuPage SetMatratterCacheList(List<Matratt> matratteradded, HttpRequest request, HttpResponse response)
        {
            return _cache.SetMatratterCacheList(matratteradded, request, response);
        }
        public MenuPage CustomerBasket(int id, HttpRequest request, HttpResponse response)
        {

            var matratteradded =  GetMatratterCacheList(id, "1", request, response);            
            var menumodel = SetMatratterCacheList(matratteradded, request, response);            
            menumodel.mattratttyper = GetMatratttyper();
            menumodel = CheckBonusPoints(request,menumodel);
            /*var customerid = _cache.GetCustomerIDCache(request);
            menumodel.Customer = _repositoryCustomer.GetById(customerid);
            menumodel.Matratteradded.OrderBy(r => r.Pris);
            if(Convert.ToInt32(menumodel.Customer.BonusPoints) >= 100)
            {
                menumodel.Matratteradded[0].Pris = 0;
            }*/
            return menumodel;
        }
        public MenuPage CheckBonusPoints(HttpRequest request,MenuPage menumodel)
        {
            var customerid = _cache.GetCustomerIDCache(request);
            menumodel.Customer = _repositoryCustomer.GetAll().FirstOrDefault(r => r.KundId == customerid);
            
            if (Convert.ToInt32(menumodel.Customer.BonusPoints) >= 100 && menumodel.Matratteradded.Count != 0)
            {
                menumodel.Matratteradded.OrderBy(r => r.Pris);
                menumodel.Matratteradded[0].Pris = 0;
                menumodel.Matratteradded.OrderBy(r => r.Pris);
            }
            return menumodel;
        }
        public MenuPage RemoveItemCustomerBasket(int id, int count, HttpRequest request, HttpResponse response)
        {
            var matratteradded = GetMatratterCacheList(id, "2", request, response);
            matratteradded.RemoveAt(count);
            var menumodel = SetMatratterCacheList(matratteradded, request, response);       
            menumodel = CheckBonusPoints(request, menumodel);
            return menumodel;
        }
        public MenuPage MenuPageData(HttpRequest request, HttpResponse response)
        {
            var id = _cache.GetCustomerIDCache(request);
            var model = GetMenuInfo();
            var matratteradded = GetMatratterCacheList(id, "2", request, response);
            if (matratteradded.Count != 0)
            {
                
                matratteradded.Add(model.matratt);
                model.Matratteradded = matratteradded;
            }
            
            model.mattratttyper = GetMatratttyper();
            model = CheckBonusPoints(request, model);
            return model;
        }
        public MenuPage CheckMatrattsValidation(MenuPage model)
        {
            var matratt = _repository.CheckMatrattsValidation(model);
            if (matratt != null)
            {
                model.MatrattsnamnTaken = true;
                model.NewDish.MatrattnamnTaken = true;
            }
            return model;
        }
        

    }
}
