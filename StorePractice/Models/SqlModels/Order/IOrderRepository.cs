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
        public void AddOrder(Order order);
    }
}
