using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models;

namespace StorePractice.Models.SqlModels
{
    public interface IOrderRepository
    {
        public IQueryable<Order> GetOrders();
        public void CreateOrder(Order order);
        public void RemoveOrder(Order order);
        public void UpdateOrder(Order order, int orderId);
    }
}
