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
                var ingrediens = db.Produkt.FirstOrDefault(r => r.ProduktNamn == produkt.ProduktNamn);
                if (ingrediens != null)
                {
                    return false;
                }
                db.Produkt.Add(produkt);
                db.SaveChanges();
                return true;
            }
        }
        public MenuPage CheckMatrattsValidation(MenuPage model)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var matratt = db.Matratt.FirstOrDefault(r => r.MatrattNamn == model.NewDish.Matratt.MatrattNamn);
                if (matratt != null)
                {
                    model.MatrattsnamnTaken = true;
                    model.NewDish.MatrattnamnTaken = true;
                }

            }
            return model;
        }
        public void SaveIngrediensesToDish(Matratt dish)
        {
            _context.Matratt.Update(dish).Context.SaveChanges();
        }
        public Matratt GetDishByName(string name)
        {
            return _context.Matratt.FirstOrDefault(r => r.MatrattNamn == name);
        }

       public Matratt GetDishById(int id)
        {
            return _context.Matratt.FirstOrDefault(r => r.MatrattId == id);
        }

        public List<Produkt> GetIngrdiensInMatratt(int id)
        {
            return _context.MatrattProdukt.Include(r => r.Produkt)
                                          .Where(r => r.MatrattId == id)
                                          .Select(r => r.Produkt).ToList();
        }

        public void RemoveIngrediens(int id)
        {
            _context.Remove(_context.MatrattProdukt.Include(r => r.Produkt)
                                                   .Select(r => r.Produkt)
                                                   .Where(r => r.ProduktId == id)
                                                   .FirstOrDefault())
                                                   .Context.SaveChanges(); 
        }
        public void Delete(int id)
        {
            var dish = _context.Matratt.Include(r => r.MatrattProdukt)
                                       .Include(r => r.BestallningMatratt)
                                       .FirstOrDefault(r => r.MatrattId == id);
            _context.Matratt.Remove(dish).Context.SaveChanges();
        }
        public void Create(Matratt dish)
        {
            _context.Matratt.Add(dish).Context.SaveChanges();
        }
        public void Update(UpdateDishViewModel model)
        {
            
            using (TomasosContext db = new TomasosContext())
            {
                var matratt = db.Matratt.FirstOrDefault(r => r.MatrattId == model.id);
                var oldmatrattsprodukts = db.MatrattProdukt.Where(r => r.MatrattId == model.id).ToList();


                var matrattprodukts = new List<MatrattProdukt>();
                matratt.MatrattNamn = model.Matrattnamn;
                foreach (var item in model.NewSelectedListItem)
                {

                    var matrattprodukt = new MatrattProdukt();
                    matrattprodukt.MatrattId = model.id;
                    matrattprodukt.ProduktId = item;
                    matrattprodukts.Add(matrattprodukt);
                }
                matratt.MatrattTyp = model.MatrattstypID;
                matratt.Pris = model.Pris;

                if (model.NewSelectedListItem.Count == 0)
                {
                    db.Matratt.Update(matratt);
                    db.SaveChanges();
                }
                else
                {
                    db.MatrattProdukt.RemoveRange(oldmatrattsprodukts);
                    db.Matratt.Update(matratt);
                    db.MatrattProdukt.AddRange(matrattprodukts);
                    db.SaveChanges();
                }
            }
            
        }
        public void UpdateDishIngredienses(Matratt matrattbyid)
        {
            _context.Update(matrattbyid).Context.SaveChanges();
        }
    }
}
