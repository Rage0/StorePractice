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
        private IProductRepository productRepository;
        private LineCategories categories; // Session categories
        public int PageSize { get; set; } = 8;

        public ProductController(IProductRepository repo, LineCategories line)
        {
            productRepository = repo;
            categories = line;
        }

        public ViewResult List(int pageNow = 1)
        {

            return View(new PageAndProductViewModel
            {
                Products = productRepository.GetProducts()
                .ToList()
                .OrderBy(p => p.ProductID)
                .Where(p => SortCollectionCategory(p) ||
                        categories.Categories.Count == 0)
                /*.SortByArray(categories.Categories)*/
                .Skip((pageNow - 1) * PageSize)
                .Take(PageSize)
                ,

                Pages = new Page
                {
                    TotalItem = categories.Categories.Count == 0 ? productRepository.GetProducts().Count() :
                                 productRepository.GetProducts().ToList().SortByArray(categories.Categories).Count(),

                    PageSize = this.PageSize,
                },

                CurrentCategory = categories
            }) ;
        }

        public IActionResult ProductProfile(int productId)
        {
            return View(
                productRepository.GetProducts().FirstOrDefault(p => p.ProductID == productId));
        }

        private bool SortCollectionCategory(Product product)
        {
            /*Console.WriteLine($"intersect {product.Category.Intersect(categories.Categories).Any()}");*/

            var productCategories = product.Category.GetEnumerator();

            while (productCategories.MoveNext())
            {
                /*Console.WriteLine($"product: {product.Name}");*/
                foreach (Category category in categories.Categories)
                {
                    /*Console.WriteLine($"Equals: {productCategories.Current.Name == category.Name}");
                    Console.WriteLine($"Category product: {productCategories.Current.Name}; Category: {category.Name}\n");*/
                    if (productCategories.Current.Name == category.Name)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}