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
        Matratt GetDishById(int id);
        bool AddIngrediens(Produkt produkt);
        Matratt GetDishByName(string name);
        void UpdateDishIngredienses(Matratt matrattbyid);
        List<Produkt> GetIngrdiensInMatratt(int id);
        void RemoveIngrediens(int id);
        MenuPage CheckMatrattsValidation(MenuPage model);
        void UpdateIngrediensesInDish(Matratt model);
        void DeleteMatrattProduktList(List<MatrattProdukt> model);
        List<MatrattProdukt> GetOldIngredienses(int id);
        void Create(Matratt dish);
        void Update(Matratt dish);
        void Delete(int id);
    }
}
