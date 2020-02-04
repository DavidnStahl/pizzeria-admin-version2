using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;
using System.Data.SqlClient;

namespace TomasosPizzeriaUppgift.Models.Repository
{
    public class DBRepositoryDishes : IRepositoryDishes
    {
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

        public void CreateDish(NewDishViewModel model)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var matratt = new Matratt()
                {
                    MatrattNamn = model.Matratt.MatrattNamn,
                    Beskrivning = model.Matratt.Beskrivning,
                    MatrattTyp = model.Matratt.MatrattTyp,
                    Pris = model.Matratt.Pris
                };
                db.Matratt.Add(matratt);
                db.SaveChanges();

                matratt = db.Matratt.FirstOrDefault(r => r.MatrattNamn == model.Matratt.MatrattNamn);
                foreach (var item in model.SelectedListItem)
                {
                    var matrattprodukt = new MatrattProdukt();
                    matrattprodukt.MatrattId = matratt.MatrattId;
                    matrattprodukt.ProduktId = item;
                    matratt.MatrattProdukt.Add(matrattprodukt);

                }
                db.Matratt.Update(matratt);
                db.SaveChanges();
            }
        }

        public void DeleteDish(int dishID)
        {
            using (TomasosContext db = new TomasosContext())
            {

                var matratt = db.Matratt.FirstOrDefault(r => r.MatrattId == dishID);
                var matrattprodut = db.MatrattProdukt.Where(r => r.MatrattId == dishID).ToList();
                var bestallningmatratt = db.BestallningMatratt.Where(r => r.MatrattId == dishID).ToList();

                db.BestallningMatratt.RemoveRange(bestallningmatratt);
                db.MatrattProdukt.RemoveRange(matrattprodut);
                db.Matratt.Remove(matratt);
                db.SaveChanges();
            }
        }

        public List<Produkt> GetIngrdiensInMatratt(Matratt matratt)
        {
            var produkts = new List<Produkt>();

            using (TomasosContext db = new TomasosContext())
            {
                var matrattprodukt = db.MatrattProdukt.Where(r => r.MatrattId == matratt.MatrattId).ToList();
                foreach (var item in matrattprodukt)
                {
                    var produkt = new Produkt();
                    var produktnamn = db.Produkt.FirstOrDefault(r => r.ProduktId == item.ProduktId);
                    produkt.ProduktId = item.ProduktId;
                    produkt.ProduktNamn = produktnamn.ProduktNamn;

                    produkts.Add(produkt);
                }
                return produkts;

            }
        }

        public void RemoveIngrediens(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {

                var matrattprodut = db.MatrattProdukt.Where(r => r.ProduktId == id).ToList();
                var produkt = db.Produkt.FirstOrDefault(r => r.ProduktId == id);

                db.MatrattProdukt.RemoveRange(matrattprodut);
                db.Produkt.Remove(produkt);
                db.SaveChanges();
            }
        }

        public void UpdateDish(UpdateDishViewModel model)
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
    }
}
