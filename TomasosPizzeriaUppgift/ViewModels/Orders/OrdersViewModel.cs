using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class OrdersViewModel
    {

        public List<Bestallning> Orders { get; set; }


        public OrdersViewModel()
        {
            Orders = new List<Bestallning>();
        }
    }
}
