using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;
using System.Data.SqlClient;
using System.Data.Entity;


namespace TomasosPizzeriaUppgift.Models.Repository
{
    public class DBRepositoryCustomers : IRepositoryCustomers
    {

        private readonly TomasosContext _context = new TomasosContext();
       
        public void Create(Kund customer)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Kund.Add(customer).Context.SaveChanges();
            }

        }
        public void Update(Kund customer)
        {
            using (TomasosContext db = new TomasosContext())
            {
               db.Kund.Update(customer).Context.SaveChanges();
            }
        }
        public List<Kund> GetAll()
        {
            using(TomasosContext db = new TomasosContext())
            {
                return db.Kund.ToList();
            }

        }
        public void Delete(Kund customer)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Kund.Remove(_context.Kund.Include(b => b.Bestallning).FirstOrDefault(r => r.KundId == customer.KundId)).Context.SaveChanges();
            }
        }
    }
}
