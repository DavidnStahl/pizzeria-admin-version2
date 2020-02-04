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
    public class RoleAdminService
    {
        
        private static RoleAdminService instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryCustomers _repository;
        private ICache _cache;
        private IIdentity _identity;



        public static RoleAdminService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new RoleAdminService();
                        instance._repository = new DBRepositoryCustomers();
                        instance._cache = new CacheLogic();
                        instance._identity = new IdentityUserLogic();

                    }
                    return instance;

                }
            }
        }

        public RoleAdminService()
        {
        }
        public UsersViewModel GetAllUsers(RoleManager<IdentityRole> roleManager,string roleToSearch)
        {
            var customers = _repository.GetCustomers();
            var result = _identity.GetAllUsers(customers, roleManager,roleToSearch);
            return result.Result;


        }
        public void DeleteUser(string userName, HttpRequest request, HttpResponse response)
        {
            _repository.DeleteUser(userName);
            _cache.ResetCookie(request, response);
        }
        public void ChangeRoleTypeUser(string changeRoleTo, string id, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _identity.UpdateRoleForUser(changeRoleTo, id, userManager, roleManager);
        }
        public UpdateRoleViewModel GetUserIdentityInfoByUsername(string userName, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var result = _identity.GetUserIdentityByUsername(userName, userManager, roleManager);
            var model = result.Result;
            return model;

        }
        public bool IdentityCreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model)
        {
            var result = _identity.CreateRole(roleManager, model);
            if (result.Result.Succeeded) { return true; }
            return false;
        }
        public bool Identity(string option, LoginViewModel loginViewModel, Kund model, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpRequest request, HttpResponse response, System.Security.Claims.ClaimsPrincipal user, RoleManager<IdentityRole> roleManager)
        {
            if (option == "create")
            {
                var result1 = _identity.CreateUserIdentity(model, userManager, signInManager, request, response, roleManager);
                if (result1.Result.Succeeded) { return true; }
                return false;

            }
            else if (option == "signin")
            {
                var result2 = _identity.SignInIdentity(loginViewModel, userManager, signInManager, request, response);
                if (result2.Result.Succeeded) { return true; }
                return false;
            }

            var result3 = _identity.UpdateUserIdentity(model, userManager, signInManager, request, response, user);
            if (result3.Result.Succeeded) { return true; }
            return false;

        }
    }
}
