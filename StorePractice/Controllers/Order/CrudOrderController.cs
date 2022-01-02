using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using System.Linq;

namespace StorePractice.Controllers
{
    [Authorize]
    public class CrudOrderController : Controller
    {
        private EfOrderRepository _orderRepository;
        public CrudOrderController(EfOrderRepository orders)
        {
            _orderRepository = orders;
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Order order = _orderRepository.GetOrders().FirstOrDefault(o => o.OrderID == id);
            if (order != null)
            {
                _orderRepository.RemoveOrder(order);
            }
            return RedirectToAction("Order", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Edit(Order order, int orderId)
        {
            _orderRepository.UpdateOrder(order, orderId);

            return RedirectToAction("Order", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Create(Order order)
        {
            if (order.OrderID == 0)
            {
                _orderRepository.CreateOrder(order);
            }

            return RedirectToAction("Order", "Admin");
        }
    }
}
