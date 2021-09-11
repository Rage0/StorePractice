using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models.SqlModels
{
    public class EfOrderRepository : IOrderRepository
    {
        private ApplicationsContext _repository;
        public EfOrderRepository(ApplicationsContext repo)
        {
            _repository = repo;
        }

        public void AddOrder(Order order)
        {
            _repository.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _repository.Orders.Add(order);
            }
            _repository.SaveChanges();
        }

        public IQueryable<Order> GetOrders() => _repository.Orders
            .Include(o => o.Lines)
            .ThenInclude(o => o.Product);
    }
}
