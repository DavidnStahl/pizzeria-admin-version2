using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class MenuPAge
    {
        public Matratt Matratt { get; set; }
        public MenuPAge()
        {
            Matratt = new Matratt();
        }
    }
}
