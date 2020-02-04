using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    interface IRepositoryCustomers
    {
        Kund GetById(int id);
        void SaveUser(Kund user);
        Kund GetUserId(Kund customer);
        Kund GetCustomerByUsername(string username);
        void UpdateUser(Kund user, int customerid);
        Kund CheckUserName(Kund customer);
        Kund CheckCustomerUsernamePassword(LoginViewModel model);
        List<Kund> GetCustomers();
        void DeleteUser(string userName);
    }
}
