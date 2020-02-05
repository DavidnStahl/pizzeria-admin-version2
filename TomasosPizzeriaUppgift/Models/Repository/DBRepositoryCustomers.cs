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
            _context.Kund.Add(customer).Context.SaveChanges(); 
        }
        public void Update(Kund customer)
        {
            _context.Kund.Update(customer).Context.SaveChanges();
        }
        public IQueryable<Kund> GetAll()
        {
            return _context.Kund;
        }
        public void Delete(Kund customer)
        {
            _context.Kund.Remove(_context.Kund.Include(b => b.Bestallning).FirstOrDefault(r => r.KundId == customer.KundId)).Context.SaveChanges();
        }
    }
}
