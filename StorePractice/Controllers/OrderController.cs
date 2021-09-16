using Microsoft.AspNetCore.Mvc;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.ViewModels;

namespace StorePractice.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private Cart _sessionCart;
        public OrderController(IOrderRepository repo, Cart cart)
        {
            _orderRepository = repo;
            _sessionCart = cart;
        }

        public IActionResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            if (order.OrderID == 0)
            {
                order.Lines = _sessionCart.GetItem.ToArray();
                _orderRepository.AddOrder(order);
                TempData.Add("Message", $"Your order added in a list orders {order.Name}");
            }
            
            return RedirectToAction(nameof(ClearCart));
        }


        public RedirectToActionResult ClearCart()
        {
            _sessionCart.Clear();
            return RedirectToAction("List", "Product");
        }
    }
}
