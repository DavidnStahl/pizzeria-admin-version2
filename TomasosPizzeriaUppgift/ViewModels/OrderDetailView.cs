using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class OrderDetailView
    {
        public OrderDetailView()
        {
            Order = new Bestallning();
            Matratter = new List<Matratt>();

        }

        public Bestallning Order { get; set; }
        public List<Matratt> Matratter { get; set; }
    }
}
