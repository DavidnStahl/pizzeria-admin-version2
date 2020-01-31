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
            BestallningMatrattList = new List<BestallningMatratt>();
            MatratterList = new List<Matratt>();
        }
        public List<BestallningMatratt> BestallningMatrattList { get; set; }
        public List<Matratt> MatratterList { get; set; }
    }
}
