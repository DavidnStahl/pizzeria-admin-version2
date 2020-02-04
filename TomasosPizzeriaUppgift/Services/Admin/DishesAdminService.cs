using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.Models;
using TomasosPizzeriaUppgift.Models.Identity;
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
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;


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
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

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
            var matratt = MenuService.Instance.GetMatratterById(id);
            var selectedListItem = _repository.GetIngrdiensInMatratt(matratt);
            var model = new UpdateDishViewModel()
            {
                Matrattnamn = matratt.MatrattNamn,
                MatrattstypID = matratt.MatrattTyp,
                Pris = matratt.Pris

            };
            model.Mattratttyper = menu.mattratttyper;
            model.MatrattstypID = matratt.MatrattTyp;
            model.SelectedListItem = selectedListItem;
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
            _repository.UpdateDish(model);
        }
        public bool AddIngrediens(Produkt produkt)
        {
            return _repository.AddIngrediens(produkt);
        }
        public void DeleteDish(int dishID)
        {
            _repository.DeleteDish(dishID);
        }
        public void CreateDish(NewDishViewModel model)
        {
            _repository.CreateDish(model);
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
