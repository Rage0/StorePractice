using Microsoft.AspNetCore.Mvc;
using StorePractice.Models.SqlModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.ViewModels;
using StorePractice.Models;

namespace StorePractice.Controllers
{
    public class AdminController : Controller
    {
        private EfOrderRepository _orderRepository;
        private EfProductRepository _productRepository;
        private EfCategoryRepository _categoryRepository;

        public int PageSize = 40;

        public AdminController(EfOrderRepository repo, EfProductRepository productRepository, EfCategoryRepository categoryRepository)
        {
            _orderRepository = repo;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
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



    }
}
