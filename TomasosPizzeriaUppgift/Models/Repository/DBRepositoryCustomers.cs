using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;
using System.Data.SqlClient;

namespace TomasosPizzeriaUppgift.Models.Repository
{
    public class DBRepositoryCustomers : IRepositoryCustomers
    {
        public Kund CheckCustomerUsernamePassword(LoginViewModel model)
        {
            var customer = new Kund();
            using (TomasosContext db = new TomasosContext())
            {
                customer = db.Kund.FirstOrDefault(c => c.AnvandarNamn == model.Username && c.Losenord == model.Password);
            }
            return customer;
        }

        public Kund CheckUserName(Kund customer)
        {
            var kund = new Kund();
            using (TomasosContext db = new TomasosContext())
            {
                kund = db.Kund.FirstOrDefault(r => r.AnvandarNamn == customer.AnvandarNamn);
            }
            return kund;
        }

        public void DeleteUser(string userName)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var user = db.Kund.FirstOrDefault(r => r.AnvandarNamn == userName);
                var useridentity = db.Users.FirstOrDefault(r => r.UserName == userName);
                db.Kund.Remove(user);
                db.Users.Remove(useridentity);
                db.SaveChanges();
            }
        }

        public Kund GetById(int id)
        {
            var model = new Kund();
            using (TomasosContext db = new TomasosContext())
            {
                model = db.Kund.FirstOrDefault(ratt => ratt.KundId == id);
            }
            return model;
        }

        public Kund GetCustomerByUsername(string username)
        {
            var kund = new Kund();
            using (TomasosContext db = new TomasosContext())
            {
                kund = db.Kund.FirstOrDefault(r => r.AnvandarNamn == username);
            }
            return kund;
        }

        public List<Kund> GetCustomers()
        {
            using (TomasosContext db = new TomasosContext())
            {
                return db.Kund.ToList();
            }
        }

        public Kund GetUserId(Kund customer)
        {
            var user = new Kund();
            using (TomasosContext db = new TomasosContext())
            {
                user = db.Kund.FirstOrDefault(r => r.AnvandarNamn == customer.AnvandarNamn && r.Losenord == customer.Losenord);
            }

            return user;
        }

        public void SaveUser(Kund user)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Kund.Add(user);
                db.SaveChanges();
            }
        }

        public void UpdateUser(Kund user, int customerid)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var customer = GetById(customerid);

                customer.Namn = user.Namn;
                customer.Gatuadress = user.Gatuadress;
                customer.Postnr = user.Postnr;
                customer.Postort = user.Postort;
                customer.Email = user.Email;
                customer.Telefon = user.Telefon;
                customer.AnvandarNamn = user.AnvandarNamn;
                customer.Losenord = user.Losenord;
                db.Kund.Update(customer);
                db.SaveChanges();
            }
        }
    }
}
