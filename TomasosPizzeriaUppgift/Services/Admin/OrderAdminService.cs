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
    public class OrderAdminService
    {
        
        private static OrderAdminService instance = null;
        private static readonly Object padlock = new Object();
        private IRepositoryOrders _repository;

        public static OrderAdminService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new OrderAdminService();
                        instance._repository = new DBRepositoryOrders();
                    }
                    return instance;

                }
            }
        }

        public OrderAdminService()
        {
        }
        public OrdersViewModel GetOrders(int id)
        {
            if (id == 1) return _repository.GetOrdersAllOrders();
            if (id == 2) return _repository.GetOrdersDelivered();

            return _repository.GetOrdersUnDelivered();
        }
        public OrderDetailView OrderDetailView(int orderid)
        {
            return _repository.GetOrderDetail(orderid);
        }
        public void DeliverOrder(int orderid)
        {
            _repository.DeliverOrder(orderid);
        }
        public void DeleteOrder(int orderid)
        {
            _repository.DeleteOrder(orderid);
        }

    }
}
