using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models.SqlModels
{
    public class EfOrderRepository : IOrderRepository
    {
        private ApplicationsContext repository;
        public EfOrderRepository(ApplicationsContext repo)
        {
            repository = repo;
        }

        public void AddOrder(Order order)
        {
            repository.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                repository.Orders.Add(order);
            }
            repository.SaveChanges();
        }

        public IQueryable<Order> GetOrders() => repository.Orders
            .Include(o => o.Lines)
            .ThenInclude(o => o.Product);
    }
}
