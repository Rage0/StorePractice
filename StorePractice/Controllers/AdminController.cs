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
    [Authorize(Roles = "Admin")]
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
            IQueryable<Order> orders = _orderRepository.GetOrders();
            return View(new PageAndObjectDBViewModel { 
                Orders = orders
                .OrderBy(o => o.OrderID)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = orders.Count(),

                    PageSize = this.PageSize,

                },

                CurrentPage = pageNow,

                ActionUrl = "Order"
            });
        }

        public ViewResult Product(int pageNow = 1)
        {
            IQueryable<Product> products = _productRepository.GetProducts();
            return View(new PageAndObjectDBViewModel
            {
                Products = products
                .OrderBy(p => p.ProductID)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = products.Count(),

                    PageSize = this.PageSize,

                },

                CurrentPage = pageNow,

                ActionUrl = "Product"
            });
        }

        public ViewResult Category(int pageNow = 1)
        {
            IQueryable<Category> categories = _categoryRepository.GetCategories();
            return View(new PageAndObjectDBViewModel
            {
                Categories = categories
                .OrderBy(c => c.CategoryID)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = categories.Count(),

                    PageSize = this.PageSize,

                },

                CurrentPage = pageNow,

                ActionUrl = "Category"
            });
        }

        public ViewResult Users(int pageNow = 1)
        {
            IQueryable<User> users = _userManager.Users;
            return View(new PageAndObjectDBViewModel
            {
                Users =  users
                        .OrderBy(u => u.Id)
                        .Skip((pageNow - 1) * PageSize)
                        .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = users
                    .Include(u => u.HasProducts).Count(),

                    PageSize= this.PageSize,
                },

                CurrentPage = pageNow,

                ActionUrl = "Users"

            });
        }

        public ViewResult Roles(int pageNow = 1)
        {
            ViewBag.UserManager = _userManager;

            IQueryable<IdentityRole> roles = _roleManager.Roles;
            return View(new PageAndObjectDBViewModel
            {
                Roles = roles
                .ToList()
                .OrderBy(r => r.Id)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize),

                Pages = new Page
                {
                    TotalItem = roles.Count(),

                    PageSize = this.PageSize
                },

                CurrentPage = pageNow,

                ActionUrl = "Roles"
            });
        }
    }
}
