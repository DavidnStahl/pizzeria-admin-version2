using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class OrdersViewModel
    {
        public List<Bestallning> UnDeliveredOrders { get; set; }
        public List<Bestallning> DeliveredOrders { get; set; }

        public List<Bestallning> AllOrders { get; set; }


    }
}
