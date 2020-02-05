using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.Models.IdentityLogic;
using TomasosPizzeriaUppgift.Models.Repository;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Services
{
   
    public class DishesAdminService
    {
        private static DishesAdminService instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryDishes _repository;

        public static DishesAdminService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DishesAdminService();
                        instance._repository = new DBRepositoryDishes();
                    }
                    return instance;

                }
            }
        }

        public DishesAdminService()
        {
        }
        public MenuPage CheckMatrattsValidation(MenuPage model)
        {
            return _repository.CheckMatrattsValidation(model);
        }
        public UpdateDishViewModel GetDishToUpdate(int id)
        {
            var menu = MenuService.Instance.GetMenuInfo();
            var matratt = _repository.GetDishById(id);
            var ingredienses = _repository.GetIngrdiensInMatratt(id);
            var model = new UpdateDishViewModel()
            {
                Matrattnamn = matratt.MatrattNamn,
                MatrattstypID = matratt.MatrattTyp,
                Pris = matratt.Pris
            };
            model.id = matratt.MatrattId;
            model.Mattratttyper = menu.mattratttyper;
            model.MatrattstypID = matratt.MatrattTyp;
            model.SelectedListItem = ingredienses;
            model.Ingredienses = menu.Ingredienses;
            model.id = matratt.MatrattId;
            return model;
        }
        public void RemoveIngrediens(int id)
        {
            _repository.RemoveIngrediens(id);
        }
        public void UpdateDish(UpdateDishViewModel model)
        {
            _repository.Update(model);
        }
        public bool AddIngrediens(Produkt produkt)
        {
            return _repository.AddIngrediens(produkt);
        }
        public void DeleteDish(int dishID)
        {
            _repository.Delete(dishID);
        }
        public void CreateDish(NewDishViewModel model)
        {
            var matratt = new Matratt()
            {
                MatrattNamn = model.Matratt.MatrattNamn,
                Beskrivning = model.Matratt.Beskrivning,
                MatrattTyp = model.Matratt.MatrattTyp,
                Pris = model.Matratt.Pris
            };
            _repository.Create(matratt);
            var matrattbyid = _repository.GetDishByName(model.Matratt.MatrattNamn);
            var matrattprodukt = new MatrattProdukt();
            foreach(var item in model.SelectedListItem)
            {
                matrattprodukt.MatrattId = matrattbyid.MatrattId;
                matrattprodukt.ProduktId = item;

                matrattbyid.MatrattProdukt.Add(matrattprodukt);
            }
            _repository.UpdateDishIngredienses(matrattbyid);
        }
        public MenuPage SetValidtion(MenuPage model, NewDishViewModel newModel)
        {
            if (newModel.SelectedListItem.Count == 0) { model.Ingredienslow = true; }
            if (newModel.Matratt.MatrattNamn.Length < 2) { model.Matrattsnamnlength = true; }
            if (newModel.MatrattnamnTaken == true) { model.MatrattsnamnTaken = true; }
            return model;

        }
    }
    
}
