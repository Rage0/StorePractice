﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Infrastructure;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using StorePractice.Models.ViewModels;

namespace StorePractice.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private LineCategories _sessionCategories;
        public int PageSize { get; } = 12;

        public ProductController(IProductRepository repo, LineCategories line)
        {
            _productRepository = repo;
            _sessionCategories = line;
        }

        public ViewResult List(int pageNow = 1)
        {
            return View(new PageAndProductViewModel
            {
                Products = _productRepository.GetProducts()
                .ToList()
                .OrderBy(p => p.ProductID)
                .Where(p => SortCollectionCategory(p) ||
                        _sessionCategories.Categories.Count == 0)
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize)
                ,

                Pages = new Page
                {
                    TotalItem = _sessionCategories.Categories.Count == 0 ? _productRepository.GetProducts().Count() :
                                 _productRepository.GetProducts().ToList().Where(p => SortCollectionCategory(p)).Count(),

                    PageSize = this.PageSize,
                },

                SessionCategories = _sessionCategories,

                CurrentPage = pageNow

            });
        }

        public IActionResult ProductProfile(int productId)
        {
            return View(
                _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId));
        }

        private bool SortCollectionCategory(Product product)
        {
            var productCategories = product.Categories.GetEnumerator();

            while (productCategories.MoveNext())
            {
                foreach (Category category in _sessionCategories.Categories)
                {
                    if (productCategories.Current.CategoryID == category.CategoryID)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}