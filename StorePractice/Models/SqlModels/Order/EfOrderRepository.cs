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

        public void CreateOrder(Order order)
        {
            _repository.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderID == 0)
            {
                _repository.Orders.Add(order);
            }
            _repository.SaveChanges();
        }

        public void UpdateOrder(Order order, int orderId)
        {
            Order orderForEdit = _repository.Orders.Find(orderId);

            orderForEdit.Name = order.Name;
            orderForEdit.Line1 = order.Line1;
            orderForEdit.Line2 = order.Line2;
            orderForEdit.Line3 = order.Line3;
            orderForEdit.City = order.City;
            orderForEdit.Country = order.Country;
            orderForEdit.Zip = order.Zip;
            _repository.SaveChanges();

        }

        public IQueryable<Order> GetOrders() => _repository.Orders
            .Include(o => o.Lines)
            .ThenInclude(o => o.Product);

        public void RemoveOrder(Order order)
        {
            _repository.Remove(order);
            _repository.SaveChanges();
        }
    }
}
