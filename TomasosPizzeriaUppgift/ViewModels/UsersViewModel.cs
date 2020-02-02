using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class UsersViewModel
    {
        public List<Kund> Customers { get; set; }

        public UsersViewModel()
        {
            Customers = new List<Kund>();
        }
    }
}
