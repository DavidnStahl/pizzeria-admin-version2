using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;

namespace TomasosPizzeriaUppgift.ViewModels
{
    public class CreateDishViewModel
    {
        public Matratt Matratt { get; set; }
        public CreateDishViewModel()
        {
            Matratt = new Matratt();
        }
    }
}
