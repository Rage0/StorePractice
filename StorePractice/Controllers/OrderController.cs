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

        public IActionResult EditOrCreate(int orderId)
        {
            if (orderId != 0)
            {
                Order order = _orderRepository.GetOrders().FirstOrDefault(o => o.OrderID == orderId);
                return View(order);
            }
            else
            {
                return View(new Order());
            }
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
                order.Lines = _sessionCart.GetItem.ToArray();
                _orderRepository.CreateOrder(order);
            }

            return RedirectToAction("Order", "Admin");
        }


        #region Method for User

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            if (order.OrderID == 0)
            {
                order.Lines = _sessionCart.GetItem.ToArray();
                _orderRepository.CreateOrder(order);
                TempData.Add("Message", $"Your order added in a list orders {order.Name}");
            }
            
            return RedirectToAction("ClearCart", "Cart");
        }

        #endregion
    }
}
