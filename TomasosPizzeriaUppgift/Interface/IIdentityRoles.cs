using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    interface IIdentityRoles
    {
        Task<IdentityResult> CreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model);

        void UpdateRoleForUser(string changeRoleTo, string id, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager);

    }   
        
}
