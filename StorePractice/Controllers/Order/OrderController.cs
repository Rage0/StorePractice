using Microsoft.AspNetCore.Mvc;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace StorePractice.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private EfOrderRepository _orderRepository;
        private Cart _sessionCart;
        public OrderController(EfOrderRepository repo, Cart cart)
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
        public IActionResult AddOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.OrderID == 0)
                {
                    if (order.Lines != null)
                    {
                        order.Lines = _sessionCart.GetItem.ToArray();
                        order.OwnerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                        _orderRepository.CreateOrder(order);
                        TempData.Add("Message", $"Your order added in a list orders {order.Name}");
                        return RedirectToAction("ClearCart", "Cart");
                    }
                    else
                    {
                        TempData.Add("Message", $"Product not selected");
                        return RedirectToAction("List", "Product");
                    }
                    
                }
                else
                {
                    return RedirectToAction("EditOrCreate", "Order", order.OrderID);
                }
            }
            return RedirectToAction("Checkout", "Order");

        }

    }
}
