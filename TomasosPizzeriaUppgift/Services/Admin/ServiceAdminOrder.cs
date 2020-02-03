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
    public class ServiceAdminOrder
    {
        
        private static ServiceAdminOrder instance = null;
        private static readonly Object padlock = new Object();
        private IRepository _repository;
        private ICache _cache;
        private IIdentityUser _identityUser;
        private IIdentityRoles _identityRole;



        public static ServiceAdminOrder Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServiceAdminOrder();
                        instance._repository = new DBRepository();
                        instance._cache = new CacheLogic();
                        instance._identityUser = new IdentityUserLogic();
                        instance._identityRole = new IdentityRoleLogic();

                    }
                    return instance;

                }
            }
        }

        public ServiceAdminOrder()
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
