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
        public void AddAdminRoleToUser()
        {
            throw new NotImplementedException();
        }

        public void AddBonusUserRoleToUser()
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResult> CreateRole(RoleManager<IdentityRole> roleManager, CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = model.RoleName
            };

            IdentityResult result = await roleManager.CreateAsync(identityRole);

            return result;
        }

        public void UpdateRoleForUser()
        {
            throw new NotImplementedException();
        }
    }
}
