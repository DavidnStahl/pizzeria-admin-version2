using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.Interface;
using TomasosPizzeriaUppgift.ViewModels;
using System.Data.SqlClient;

namespace TomasosPizzeriaUppgift.Models.Repository
{
    public class DBRepositoryOrders : IRepositoryOrders
    {
        private readonly TomasosContext _context = new TomasosContext();
        public OrdersViewModel GetOrdersAllOrders()
        {
            var model = new OrdersViewModel();
            using (TomasosContext db = new TomasosContext())
            {
                model.Orders = db.Bestallning.OrderByDescending(r => r.BestallningDatum).ToList();
                foreach (var order in model.Orders)
                {

                    order.Kund = db.Kund.FirstOrDefault(r => r.KundId == order.KundId);
                }

            }
            return model;
        }
        public OrdersViewModel GetOrdersDelivered()
        {
            var model = new OrdersViewModel();
            using (TomasosContext db = new TomasosContext())
            {
                model.Orders = db.Bestallning.Where(r => r.Levererad == true)
                                             .OrderByDescending(r => r.BestallningDatum)
                                             .ToList();

                foreach (var order in model.Orders)
                {
                    var customer = new Kund();

                    order.Kund = db.Kund.FirstOrDefault(r => r.KundId == order.KundId);
                }

            }
            return model;
        }

        public OrderDetailView GetOrderDetail(int id)
        {
            var model = new OrderDetailView();
            using (TomasosContext db = new TomasosContext())
            {
                model.Order = db.Bestallning.FirstOrDefault(r => r.BestallningId == id);
                model.Order.Kund = db.Kund.FirstOrDefault(r => r.KundId == model.Order.KundId);
                model.Order.BestallningMatratt = db.BestallningMatratt.Where(r => r.BestallningId == id).ToList();
                model.Matratter = db.Matratt.ToList();

            }
            return model;

        }

        public OrdersViewModel GetOrdersUnDelivered()
        {
            var model = new OrdersViewModel();
            using (TomasosContext db = new TomasosContext())
            {
                model.Orders = db.Bestallning.Where(r => r.Levererad == false)
                                             .OrderByDescending(r => r)
                                             .ToList();

                foreach (var order in model.Orders)
                {
                    var customer = new Kund();

                    order.Kund = db.Kund.FirstOrDefault(r => r.KundId == order.KundId);
                }
            }
            return model;
        }

        public void DeliverOrder(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {
                var order = db.Bestallning.FirstOrDefault(r => r.BestallningId == id);
                order.Levererad = true;
                db.Bestallning.Update(order);
                db.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            using (TomasosContext db = new TomasosContext())
            {

                var order = db.Bestallning.FirstOrDefault(r => r.BestallningId == id);
                var bestallningmatratt = db.BestallningMatratt.Where(r => r.BestallningId == id);


                db.BestallningMatratt.RemoveRange(bestallningmatratt);

                db.Bestallning.Remove(order);
                db.SaveChanges();
            }
        }

        
    }
}
