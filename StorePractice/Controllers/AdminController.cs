using Microsoft.AspNetCore.Mvc;
using StorePractice.Models.SqlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.ViewModels;

namespace StorePractice.Controllers
{
    public class AdminController : Controller
    {
        private IOrderRepository _orderRepository;
        public AdminController(IOrderRepository repo)
        {
            _orderRepository = repo;
        }

        public ViewResult Index() => View(_orderRepository.GetOrders());
    }
}
