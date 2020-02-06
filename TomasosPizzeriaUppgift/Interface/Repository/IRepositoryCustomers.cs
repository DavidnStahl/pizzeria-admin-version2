using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    public interface IRepositoryCustomers
    {        
        void Create(Kund customer);
        List<Kund> GetAll();
        void Update(Kund customer);
        void Delete(Kund customer);
        
    }
}
