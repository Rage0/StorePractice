using Microsoft.AspNetCore.Mvc;
using StorePractice.Models.SqlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.ViewModels;
using StorePractice.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace StorePractice.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private EfOrderRepository _orderRepository;
        private EfProductRepository _productRepository;
        private EfCategoryRepository _categoryRepository;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public int PageSize = 40;

        public AdminController(
            EfOrderRepository orderRepository,
            EfProductRepository productRepository,
            EfCategoryRepository categoryRepository,
            UserManager<User> userRepository,
            RoleManager<IdentityRole> roleManager
            )
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _userManager = userRepository;
            _roleManager = roleManager;
        }

        public ViewResult Order(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel { 
                Orders = _orderRepository.GetOrders()
                .OrderBy(o => o.OrderID)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = _orderRepository.GetOrders().Count(),

                    PageSize = this.PageSize,

                },

                CurrentPage = pageNow,

                ActionUrl = "Order"
            });
        }

        public ViewResult Product(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel
            {
                Products = _productRepository.GetProducts()
                .OrderBy(p => p.ProductID)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = _productRepository.GetProducts().Count(),

                    PageSize = this.PageSize,

                },

                CurrentPage = pageNow,

                ActionUrl = "Product"
            });
        }

        public ViewResult Category(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel
            {
                Categories = _categoryRepository.GetCategories()
                .OrderBy(c => c.CategoryID)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = _categoryRepository.GetCategories().Count(),

                    PageSize = this.PageSize,

                },

                CurrentPage = pageNow,

                ActionUrl = "Category"
            });
        }

        public ViewResult Users(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel
            {
                Users = _userManager.Users
                        .OrderBy(u => u.Id)
                        .Skip((pageNow - 1) * PageSize)
                        .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = _userManager.Users
                    .Include(u => u.HasProducts).Count(),

                    PageSize= this.PageSize,
                },

                CurrentPage = pageNow,

                ActionUrl = "Users"

            });
        }

        public ViewResult Roles(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel
            {
                Roles = _roleManager.Roles
                .OrderBy(r => r.Id)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = _roleManager.Roles.Count(),

                    PageSize = this.PageSize
                },

                CurrentPage = pageNow,

                ActionUrl = "Roles"
            });
        }
    }
}
