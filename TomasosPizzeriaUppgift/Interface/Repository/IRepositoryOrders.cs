using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TomasosPizzeriaUppgift.ViewModels;

namespace TomasosPizzeriaUppgift.Interface
{
    interface IRepositoryOrders
    {
        OrdersViewModel GetOrdersDelivered();
        OrdersViewModel GetOrdersUnDelivered();
        OrdersViewModel GetOrdersAllOrders();
        OrderDetailView GetOrderDetail(int id);
        void DeliverOrder(int id);
        void DeleteOrder(int id);
    }
}
