using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;


namespace TomasosPizzeriaUppgift.ViewModels
{
    public class NewDishViewModel
    {
        public Matratt Matratt { get; set; }
        public List<int> SelectedListItem{ get; set; }

        public bool MatrattnamnTaken { get; set; }

        public NewDishViewModel()
        {
            SelectedListItem = new List<int>();
            Matratt = new Matratt();
        }   
    }
}
