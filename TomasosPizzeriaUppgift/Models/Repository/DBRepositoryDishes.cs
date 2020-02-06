using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;
using System.Data.SqlClient;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace TomasosPizzeriaUppgift.Models.Repository
{
    public class DBRepositoryDishes : IRepositoryDishes
    {
        private readonly TomasosContext _context = new TomasosContext();
        public bool AddIngrediens(Produkt produkt)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Produkt.FirstOrDefault(r => r.ProduktNamn == produkt.ProduktNamn);
                var ingrediens = db.Produkt.FirstOrDefault(r => r.ProduktNamn == produkt.ProduktNamn);
                if (ingrediens != null) { return false; }
                db.Produkt.Add(produkt).Context.SaveChanges();
                return true;
            }
       
        }
        public MenuPage CheckMatrattsValidation(MenuPage model)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var matratt = db.Matratt.FirstOrDefault(r => r.MatrattNamn == model.NewDish.Matratt.MatrattNamn);
                if (matratt != null) { model.MatrattsnamnTaken = true; model.NewDish.MatrattnamnTaken = true; }
                return model;
            }
                
        }
        public void SaveIngrediensesToDish(Matratt dish)
        {
            using (TomasosContext db = new TomasosContext())
            {
                _context.Matratt.Update(dish).Context.SaveChanges();
            }              
        }
        public Matratt GetDishByName(string name)
        {
            using (TomasosContext db = new TomasosContext())
            {
                return db.Matratt.FirstOrDefault(r => r.MatrattNamn == name);
            }

        }

       public Matratt GetDishById(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {
                return db.Matratt.Include(r => r.MatrattProdukt).FirstOrDefault(r => r.MatrattId == id);
            }
        }

        public List<Produkt> GetIngrdiensInMatratt(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {
                return db.MatrattProdukt.Include(r => r.Produkt)
                                          .Where(r => r.MatrattId == id)
                                          .Select(r => r.Produkt).ToList();
            }
        }

        public void RemoveIngrediens(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Remove(_context.MatrattProdukt.Include(r => r.Produkt)
                                                   .Select(r => r.Produkt)
                                                   .Where(r => r.ProduktId == id)
                                                   .FirstOrDefault())
                                                   .Context.SaveChanges();
            }
                 
        }
        public void Delete(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var dish = db.Matratt.Include(r => r.MatrattProdukt)
                                       .Include(r => r.BestallningMatratt)
                                       .FirstOrDefault(r => r.MatrattId == id);

                db.Matratt.Remove(dish).Context.SaveChanges();
            }
                

            
        }
        public void Create(Matratt dish)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Matratt.Add(dish).Context.SaveChanges();
            }
                
        }
        public void DeleteMatrattProduktList(List<MatrattProdukt> model)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.MatrattProdukt.RemoveRange(model);
                db.SaveChanges();
            }             
        }
        public void UpdateIngrediensesInDish(Matratt model)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Matratt.Update(model).Context.SaveChanges();
            }
        }
        public List<MatrattProdukt> GetOldIngredienses(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {
                return db.MatrattProdukt.Where(r => r.MatrattId == id).ToList();
            }
            
        }
        public void Update(Matratt model)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Matratt.Update(model).Context.SaveChanges();
            }

        }
        public void UpdateDishIngredienses(Matratt matrattbyid)
        {
            using (TomasosContext db = new TomasosContext())
            {
                db.Update(matrattbyid).Context.SaveChanges();
            }
            
        }
    }
}
