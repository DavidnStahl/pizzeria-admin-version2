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
        void AddAdminRoleToUser();
        void AddBonusUserRoleToUser();
        void UpdateRoleForUser();
    }
        
}
