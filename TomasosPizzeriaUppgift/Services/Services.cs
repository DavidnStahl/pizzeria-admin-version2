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
using TomasosPizzeriaUppgift.Models.Identity;

namespace TomasosPizzeriaUppgift.Services
{
    public class Services
    {
        private static Services instance = null;
        private static readonly Object padlock = new Object();
        private IRepository _repository;
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;


        public static Services Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Services();
                        instance._repository = new DBRepository();
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

                    }
                    return instance;

                }
            }
        }

        public Services()
        {
        }
        public Kund GetUserId(Kund customer)
        {
            return _repository.GetUserId(customer);


        }
        public Kund GetById(int id)
        {
            return _repository.GetById(id);
        }

        public MenuPage GetMenuInfo()
        {
            return _repository.GetMenuInfo();
        }
        public void SaveUser(Kund user)
        {
            _repository.SaveUser(user);
        }

        public Matratt GetMatratterById(int id)
        {
            return _repository.GetMatratterToCustomerbasket(id);
        }
        public void UserPay(List<Matratt> matratter, int userid)
        {
            _repository.SaveOrder(matratter, userid);

        }
        public void UpdateUser(Kund user, int userid, HttpRequest request, HttpResponse response)
        {
            _repository.UpdateUser(user, userid);
        }
        public List<MatrattTyp> GetMatratttyper()
        {
            return _repository.GetMatrattTyper();
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
        public List<Matratt> GetMatratterCacheList(int id, string options, HttpRequest request, HttpResponse response)
        {

            return _cache.GetMatratterCacheList(id, options, request, response);
        }
        public MenuPage SetMatratterCacheList(List<Matratt> matratteradded, HttpRequest request, HttpResponse response)
        {
            return _cache.SetMatratterCacheList(matratteradded, request, response);
        }
        public void ResetCookie(HttpRequest request, HttpResponse response)
        {
            _cache.ResetCookie(request, response);
        }
        public void PayUser(HttpRequest request, HttpResponse response)
        {
            var userid = GetCustomerIDCache(request);
            var matratteradded = _cache.PayUser(request, response);
            UserPay(matratteradded, userid);
            _cache.DeleteFoodListCache(request, response);
        }
        public MenuPage PayPage(HttpRequest request, HttpResponse response)
        {

            var matratteradded = _cache.GetMatratterToPay(request, response);
            var model = new MenuPage();
            model.Matratteradded = matratteradded;
            model.mattratttyper = GetMatratttyper();
            return model;
        }
        public MenuPage CustomerBasket(int id, HttpRequest request, HttpResponse response)
        {

            var matratteradded = GetMatratterCacheList(id, "1", request, response);
            var menumodel = SetMatratterCacheList(matratteradded, request, response);
            menumodel.mattratttyper = GetMatratttyper();
            return menumodel;
        }
        public MenuPage RemoveItemCustomerBasket(int id, int count, HttpRequest request, HttpResponse response)
        {
            var matratteradded = GetMatratterCacheList(id, "2", request, response);
            matratteradded.RemoveAt(count);
            var menumodel = SetMatratterCacheList(matratteradded, request, response);
            return menumodel;
        }
        public bool CheckUserNameIsValid(Kund user, HttpRequest request)
        {
            var customer = CheckUserName(user);
            var customerid = GetCustomerIDCache(request);
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
        public MenuPage MenuPageData(HttpRequest request, HttpResponse response)
        {
            var id = _cache.GetCustomerIDCache(request);
            var model = GetMenuInfo();
            var matratteradded = GetMatratterCacheList(id, "2", request, response);
            if(matratteradded.Count != 0)
            {
                matratteradded.Add(model.matratt);
                model.Matratteradded = matratteradded;
            }
            
            model.mattratttyper = GetMatratttyper();
            return model;
        }
        public string CheckIfInlogged(HttpRequest request)
        {
            var id = GetCustomerIDCache(request);
            if (id != 0)
            {
                return "Inloggad";
            }
            else
            {
                return "inte inloggad";
            }
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
        public bool Identity(string option,LoginViewModel loginViewModel, Kund model, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpRequest request, HttpResponse response, System.Security.Claims.ClaimsPrincipal user)
        {
            if (option == "create") 
            {
               var result1 = _identityUser.CreateUserIdentity(model, userManager, signInManager, request, response);
                if (result1.Result.Succeeded) { return true; }
                return false;

            }    
            else if (option == "signin")
            {
                var result2 = _identityUser.SignInIdentity(loginViewModel, userManager, signInManager, request, response);
                if (result2.Result.Succeeded) { return true; }
                return false;
            }
            
               var result3 = _identityUser.UpdateUserIdentity(model, userManager, signInManager, request, response, user);
               if (result3.Result.Succeeded) { return true; }
               return false;

        }
        public bool IdentityCreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model)
        {
            var result =  _identityRole.CreateRole(roleManager,model);
            if (result.Result.Succeeded) { return true; }
            return false;
        }




        public OrdersViewModel GetOrders(int id)
        {
            if (id == 1) return _repository.GetOrdersAllOrders();
            if (id == 2) return _repository.GetOrdersDelivered();

            return _repository.GetOrdersUnDelivered();
        }
        public OrderDetailView OrderDetailView(int orderid)
        {
            return _repository.GetOrderDetail(orderid);
        }
        public void DeliverOrder(int orderid)
        {
            _repository.DeliverOrder(orderid);
        }
        public void DeleteOrder(int orderid)
        {
            _repository.DeleteOrder(orderid);
        }
        public void DeleteDish(int dishID)
        {
            _repository.DeleteDish(dishID);
        }
        public void CreateDish(NewDishViewModel model)
        {
            _repository.CreateDish(model);
        }
        public MenuPage SetValidtion(MenuPage model, NewDishViewModel newModel)
        {
            if(newModel.SelectedListItem.Count == 0) { model.Ingredienslow = true;}
            if(newModel.Matratt.MatrattNamn.Length < 2) { model.Matrattsnamnlength = true; }
            if(newModel.MatrattnamnTaken == true) { model.MatrattsnamnTaken = true; }
            return model;

        }
        public MenuPage CheckMatrattsValidation(MenuPage model)
        {
            return _repository.CheckMatrattsValidation(model);
        }
        public bool AddIngrediens(Produkt produkt)
        {
           return _repository.AddIngrediens(produkt);
        }
        public UpdateDishViewModel GetDishToUpdate(int id)
        {
            var menu = GetMenuInfo();
            var matratt = GetMatratterById(id);
            var selectedListItem = _repository.GetIngrdiensInMatratt(matratt);
            var model = new UpdateDishViewModel()
            {
                Matrattnamn = matratt.MatrattNamn,
                MatrattstypID = matratt.MatrattTyp,
                Pris = matratt.Pris
            
            };
            model.Mattratttyper = menu.mattratttyper;
            model.MatrattstypID = matratt.MatrattTyp;
            model.SelectedListItem = selectedListItem;
            model.Ingredienses = menu.Ingredienses;
            model.id = matratt.MatrattId;
            return model;

        }
        public void RemoveIngrediens(int id)
        {
            _repository.RemoveIngrediens(id);
        }
        public void UpdateDish(UpdateDishViewModel model)
        {
            _repository.UpdateDish(model);
        }
    } 
}
