using Microsoft.AspNetCore.Mvc;
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
        private ProductInteraction _sessionProduct;
        public int PageSize { get; } = 12;

        public ProductController(IProductRepository repo, LineCategories line, ProductInteraction product)
        {
            _productRepository = repo;
            _sessionCategories = line;
            _sessionProduct = product;
        }

        public ViewResult List(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel
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

                CurrentPage = pageNow,

                ActionUrl = "List"

            });
        }

        public IActionResult ProductProfile(int productId)
        {
            return View(CheckProduct(productId));
        }

        public ViewResult EditOrCreate(int productId)
        {
            if (productId != 0)
            {
                Product product = CheckProduct(productId);
                return View(product);
            }
            else
            {
                return View(new Product());
            }
        }

        [HttpPost]
        public RedirectToActionResult AddProductToSession(int productId)
        {
            if (productId != 0)
            {
                Product product = CheckProduct(productId);
                _sessionProduct.AddProduct(product);
            }
            else
            {
                _sessionProduct.AddProduct(new Product());
            }

            return RedirectToAction("Categories", "Category");
        }


        #region CRUD
        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Product product = CheckProduct(id);

            if (product != null)
            {
                _productRepository.RemoveProduct(product);
            }
            
            return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Edit(Product product, int productId)
        {
            _productRepository.UpdateProduct(product, productId);

            return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Create(Product product)
        {
            _productRepository.CreateProduct(product);

            return RedirectToAction("Product", "Admin");
        }
        #endregion

        public Product CheckProduct(int productId)
        {
            return _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);
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