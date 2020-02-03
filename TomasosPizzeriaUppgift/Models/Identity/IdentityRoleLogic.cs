using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Models.Identity
{
    public class IdentityRoleLogic : IIdentityRoles
    {

        public async Task<IdentityResult> CreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };

            IdentityResult result = await roleManager.CreateAsync(identityRole);

            return result;
        }

        public async Task<IdentityResult> UpdateRoleForUser(RoleManager<IdentityRole> roleManager,UpdateRoleViewModel updaterole)
        {
            var role = await roleManager.FindByIdAsync(updaterole.UserID);
            role.Name = updaterole.RoleName;
            var result = await roleManager.UpdateAsync(role);
            return result;
        }
    }
}
