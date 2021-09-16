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
        private ICategoryRepository _categoryRepository;
        private List<string> _charCategories = new List<string>();
        private LineCategories _sessionCategories;

        public CategoryController(ICategoryRepository repo, LineCategories line)
        {
            _categoryRepository = repo;
            _sessionCategories = line;
        }

        public IActionResult Categories(string charFilter = "A")
        {
            return View(new CategoryFilterViewModel
            {
                Categories = _categoryRepository.GetCategories()
                             .Where(f => f.Name.StartsWith(charFilter)),

                CharsCategories = CharCategories()
            });
        }

        [HttpPost]
        public RedirectToActionResult AddToFilterCategories(int categoryId)
        {
            Category category = _categoryRepository.GetCategories().FirstOrDefault(c => c.CategoryID == categoryId);

            if (category != null)
            {
                _sessionCategories.AddCategory(category);
            }

            return RedirectToAction("List", "Product", new { pageNow = 1 });
        }

        [HttpPost]
        public RedirectToActionResult DeleteCategoryToFilter(int categoryId)
        {
            Category category = _categoryRepository.GetCategories().FirstOrDefault(c => c.CategoryID == categoryId);
            
            _sessionCategories.RemoveCategory(category);

            return RedirectToAction("List", "Product", new { pageNow = 1 });
        }

        [HttpPost]
        public RedirectToActionResult ClearFilter()
        {
            _sessionCategories.Clear();

            return RedirectToAction("List", "Product");
        }

        private List<string> CharCategories()
        {
            foreach (var category in _categoryRepository.GetCategories().OrderBy(f => f.Name))
            {
                var charCategory = category.Name.Substring(0, 1).ToUpper();

                if (!_charCategories.Contains(charCategory))
                {
                    _charCategories.Add(charCategory);
                }
            }
            return _charCategories;
        }
    }
}
