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

        public ViewResult Categories(string charFilter = "A")
        {
            return View(new CategoryFilterViewModel
            {
                Categories = _categoryRepository.GetCategories()
                             .Where(f => f.Name.StartsWith(charFilter)),

                CharsCategories = CharCategories()
            });
        }

        public ViewResult EditOrCreate(int categoryId)
        {
            if (categoryId != 0)
            {
                Category category = _categoryRepository.GetCategories().FirstOrDefault(c => c.CategoryID == categoryId);
                return View(category);
            }
            else
            {
                return View(new Category());
            }
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Category category = _categoryRepository.GetCategories()
                .FirstOrDefault(c => c.CategoryID == id);

            if (category != null)
            {
                _categoryRepository.RemoveCategory(category);
            }

            return RedirectToAction("Category", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Edit(Category category, int categoryId)
        {
            _categoryRepository.UpdateCategory(category, categoryId);

            return RedirectToAction("Category", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Create(Category category)
        {
            _categoryRepository.CreateCategory(category);

            return RedirectToAction("Category", "Admin");
        }

        #region Filter
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
    #endregion
}
