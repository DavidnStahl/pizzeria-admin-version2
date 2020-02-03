using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class UpdateRoleViewModel
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }

        public List<string> Roles { get; set; }
    }
}
