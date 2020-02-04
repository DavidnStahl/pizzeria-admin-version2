using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;
using System.Data.SqlClient;

namespace TomasosPizzeriaUppgift.Models.Repository
{
    public class DBRepositoryMenu : IRepositoryMenu
    {
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

        public Matratt GetMatratterToCustomerbasket(int id)
        {
            var model = new Matratt();
            using (TomasosContext db = new TomasosContext())
            {

                model = db.Matratt.FirstOrDefault(r => r.MatrattId == id);
            }
            return model;
        }

        public List<MatrattTyp> GetMatrattTyper()
        {
            var matratttyper = new List<MatrattTyp>();
            using (TomasosContext db = new TomasosContext())
            {
                matratttyper = db.MatrattTyp.ToList();
            }
            return matratttyper;
        }

        public MenuPage GetMenuInfo()
        {
            var model = new MenuPage();

            using (TomasosContext db = new TomasosContext())
            {
                model.Matratter = db.Matratt.ToList();
                model.Ingredins.MatrattProdukt = db.MatrattProdukt.ToList();
                model.Ingredienses = db.Produkt.ToList();
                model.mattratttyper = db.MatrattTyp.ToList();

            }
            return model;
        }

        public void SaveBestallningMatratter(List<Matratt> matratter)
        {
            var bestallningsmatrattlista = new List<BestallningMatratt>();
            var id = 0;
            var first = 0;
            var count = 0;
            var nymatratter = matratter.OrderBy(r => r.MatrattNamn).ToList();
            using (TomasosContext db = new TomasosContext())
            {
                var listbestallning = db.Bestallning.OrderByDescending(r => r.BestallningDatum).ToList();
                for (var i = 0; i < nymatratter.Count; i++)
                {

                    if (id != nymatratter[i].MatrattId)
                    {
                        first++;
                        var best = new BestallningMatratt();
                        id = nymatratter[i].MatrattId;
                        best.BestallningId = listbestallning[0].BestallningId;
                        best.MatrattId = nymatratter[i].MatrattId;
                        best.Antal = 1;
                        bestallningsmatrattlista.Add(best);

                    }
                    else if (id == nymatratter[i].MatrattId)
                    {
                        count = first - 1;
                        bestallningsmatrattlista[count].Antal++;

                    }
                }
                foreach (var item in bestallningsmatrattlista)
                {
                    db.Add(item);
                    db.SaveChanges();
                }
            }
        }

        public void SaveOrder(List<Matratt> matratter, int userid)
        {
            var customer = GetById(userid);
            var totalmoney = GetTotalPayment(matratter);
            var bestallning = new Bestallning()
            {
                BestallningDatum = DateTime.Now,
                KundId = customer.KundId,
                Totalbelopp = totalmoney,
                Levererad = false
            };


            using (TomasosContext db = new TomasosContext())
            {
                db.Add(bestallning);
                db.SaveChanges();
            }
            SaveBestallningMatratter(matratter);
        }
        public int GetTotalPayment(List<Matratt> matratter)
        {
            var totalmoney = 0;
            foreach (var matratt in matratter)
            {
                totalmoney += matratt.Pris;
            }
            return totalmoney;
        }
        public Kund GetById(int id)
        {
            var model = new Kund();
            using (TomasosContext db = new TomasosContext())
            {
                model = db.Kund.FirstOrDefault(ratt => ratt.KundId == id);
            }
            return model;

        }
    }
}
