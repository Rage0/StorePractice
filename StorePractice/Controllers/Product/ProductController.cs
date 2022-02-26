using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Infrastructure;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using StorePractice.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace StorePractice.Controllers
{
    public class ProductController : Controller
    {
        private EfProductRepository _productRepository;
        private LineCategories _sessionCategories;
        private EfCategoryRepository _categoryRepository;
        private UserManager<User> _userManager;
        public int PageSize { get; } = 24;

        public ProductController(EfProductRepository repo,
            LineCategories line,
            EfCategoryRepository categoryRepository,
            UserManager<User> userManager)
        {
            _productRepository = repo;
            _sessionCategories = line;
            _categoryRepository = categoryRepository;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public ViewResult List(int pageNow = 1)
        {
            return View(new PageAndObjectDBViewModel
            {
                Products =_productRepository.GetProducts()
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

        [Authorize]
        public async Task<IActionResult> ProductProfile(int productId)
        {
            Product product = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);

            ProductProfileViewModel profile = new ProductProfileViewModel
            {
                Product = product,

                User = await _userManager.FindByIdAsync(product.OwnerId),
            };

            return View(profile);
        }

        [Authorize]
        public ViewResult EditOrCreate(int productId, string returnUrl)
        {
            List<Category> HasCategories = new List<Category>();
            List<Category> HasNotCategories = new List<Category>();
            ViewBag.ReturnUrl = returnUrl;

            if (productId != 0)
            {
                Product product = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId);
                
                if (product != null)
                {
                    foreach (Category category in _categoryRepository.GetCategories())
                    {
                        if (!product.Categories.Contains(category))
                        {
                            HasNotCategories.Add(category);
                        }
                    }

                    return View(new ProductViewModel()
                    {
                        Product = product,

                        HasCategories = product.Categories,

                        HasNotCategories = HasNotCategories 
                    });
                }
                else
                {
                    return View(new ProductViewModel()
                    {
                        Product = new Product(),

                        HasCategories = new List<Category>(),

                        HasNotCategories = _categoryRepository.GetCategories().ToList()
                    });
                }
            }
            else
            {
                return View(new ProductViewModel()
                {
                    Product = new Product(),

                    HasCategories = new List<Category>(),

                    HasNotCategories = _categoryRepository.GetCategories().ToList()
                }) ;
            }
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