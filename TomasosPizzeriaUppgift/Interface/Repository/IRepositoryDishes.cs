using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    interface IRepositoryDishes
    {
        void DeleteDish(int dishID);
        void CreateDish(NewDishViewModel model);
        bool AddIngrediens(Produkt produkt);
        List<Produkt> GetIngrdiensInMatratt(Matratt matratt);
        void RemoveIngrediens(int id);
        void UpdateDish(UpdateDishViewModel model);
        MenuPage CheckMatrattsValidation(MenuPage model);
    }
}
