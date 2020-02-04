using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class ServiceAdminRole
    {
        
        private static ServiceAdminRole instance = null;
        private static readonly Object padlock = new Object();
        private IRepository _repository;
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public static ServiceAdminRole Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceAdminRole();
                        instance._repository = new DBRepository();
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

                    }
                    return instance;

                }
            }
        }

        public ServiceAdminRole()
        {
        }
        public UsersViewModel GetAllUsers(RoleManager<IdentityRole> roleManager)
        {
            var customers = _repository.GetCustomers();
            var result = _identityUser.GetAllUsers(customers, roleManager);
            return result.Result;


        }
        public void DeleteUser(string userName, HttpRequest request, HttpResponse response)
        {
            _repository.DeleteUser(userName);
            _cache.ResetCookie(request, response);
        }
        public void ChangeRoleTypeUser(string changeRoleTo, string id, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _identityRole.UpdateRoleForUser(changeRoleTo, id, userManager, roleManager);
        }
        public UpdateRoleViewModel GetUserIdentityInfoByUsername(string userName, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var result = _identityUser.GetUserIdentityByUsername(userName, userManager, roleManager);
            var model = result.Result;
            return model;

        }
        public bool IdentityCreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model)
        {
            var result = _identityRole.CreateRole(roleManager, model);
            if (result.Result.Succeeded) { return true; }
            return false;
        }
        public bool Identity(string option, LoginViewModel loginViewModel, Kund model, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpRequest request, HttpResponse response, System.Security.Claims.ClaimsPrincipal user, RoleManager<IdentityRole> roleManager)
        {
            if (option == "create")
            {
                var result1 = _identityUser.CreateUserIdentity(model, userManager, signInManager, request, response, roleManager);
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

    }
}
