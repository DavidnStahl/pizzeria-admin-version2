using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    public interface IIdentity
    {

        Task<IdentityResult> UpdateUserIdentity(Kund model, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpRequest request, HttpResponse response, System.Security.Claims.ClaimsPrincipal user);
        Task<IdentityResult> CreateUserIdentity(Kund model, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpRequest request, HttpResponse response, RoleManager<IdentityRole> roleManager);

        Task<SignInResult> SignInIdentity(LoginViewModel model, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, HttpRequest request, HttpResponse response);
        Task<UpdateRoleViewModel> GetUserIdentityByUsername(string userName, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager);
        Task<UsersViewModel> GetAllUsers(List<Kund> customers, RoleManager<IdentityRole> roleManager);
        void UpdateRoleForUser(string changeRoleTo, string id, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager);
        Task<IdentityResult> CreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model);

    }
}
