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
        private IOrderRepository repository;
        private Cart cartMain;
        public OrderController(IOrderRepository repo, Cart cart)
        {
            repository = repo;
            cartMain = cart;
        }

        public IActionResult Checkout() => View(new Order());

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            if (order.OrderID == 0)
            {
                order.Lines = cartMain.GetItem.ToArray();
                repository.AddOrder(order);
                TempData.Add("Message", $"Your order added in a list orders {order.Name}");
            }
            
            return RedirectToAction(nameof(ClearCart));
        }


        public RedirectToActionResult ClearCart()
        {
            cartMain.Clear();
            return RedirectToAction("List", "Product");
        }
    }
}
