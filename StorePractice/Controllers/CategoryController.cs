using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using StorePractice.Models.ViewModels;

namespace StorePractice.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;
        private List<string> charCategories = new List<string>();
        private LineCategories sessionCategories;

        public CategoryController(ICategoryRepository repo, LineCategories line)
        {
            repository = repo;
            sessionCategories = line;
        }

        public IActionResult Categories(string charFilter = "A")
        {
            return View(new CategoryFilterViewModel
            {
                Categories = repository.GetCategories()
                             .Where(f => f.Name.StartsWith(charFilter)),

                CharsCategories = CharCategories()
            });
        }

        [HttpPost]
        public RedirectToActionResult AddToFilterCategories()
        {
            int categoryIdToForm = Int32.Parse(Request.Form["categoryId"]);
            Category category = repository.GetCategories().FirstOrDefault(c => c.CategoryID == categoryIdToForm);

            if (category != null)
            {
                sessionCategories.AddCategory(category);
            }

            return RedirectToAction("List", "Product", new { pageNow = 1 });
        }

        [HttpPost]
        public RedirectToActionResult DeleteCategoryToFilter()
        {
            int categoryIdToForm = Int32.Parse(Request.Form["categoryId"]);
            Category category = repository.GetCategories().FirstOrDefault(c => c.CategoryID == categoryIdToForm);
            
            sessionCategories.RemoveCategory(category);

            return RedirectToAction("List", "Product", new { pageNow = 1 });
        }

        [HttpPost]
        public RedirectToActionResult ClearFilter()
        {
            sessionCategories.Clear();

            return RedirectToAction("List", "Product");
        }

        private List<string> CharCategories()
        {
            foreach (var category in repository.GetCategories().OrderBy(f => f.Name))
            {
                var charCategory = category.Name.Substring(0, 1).ToUpper();

                if (!charCategories.Contains(charCategory))
                {
                    charCategories.Add(charCategory);
                }
            }
            return charCategories;
        }
    }
}
