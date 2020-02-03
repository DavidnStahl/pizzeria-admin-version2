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

        public void UpdateRoleForUser(string changeRoleTo,string id, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using(TomasosContext db = new TomasosContext())
            {
                var userToDelete = db.UserRoles.FirstOrDefault(r=> r.UserId == id);
                db.UserRoles.Remove(userToDelete);
                db.SaveChanges();
                var userRoles = db.Roles.FirstOrDefault(r => r.Name == changeRoleTo);
                userToDelete.RoleId = userRoles.Id;
                userToDelete.UserId = id;
                db.UserRoles.Add(userToDelete);
                db.SaveChanges();


            }
            /*var role = roleManager.Roles.FirstOrDefault(r => r.Name == changeRoleTo);
            var user = await userManager.FindByIdAsync(id);
            await userManager.AddToRoleAsync(user, role.Name);*/
        }
    }
}
