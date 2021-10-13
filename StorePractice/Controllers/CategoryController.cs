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
        private SessionProduct _sessionProduct;

        public CategoryController(ICategoryRepository repo, LineCategories line, SessionProduct product)
        {
            _categoryRepository = repo;
            _sessionCategories = line;
            _sessionProduct = product;
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
                Category category = CheckCategory(categoryId);
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

        [HttpPost]
        public RedirectToActionResult AddCategoryToProduct(int categoryId)
        {
            Category category = CheckCategory(categoryId);

            if (category != null)
            {
                _sessionProduct.AddCategoryToProduct(category);
            }

            return RedirectToAction("EditOrCreate", "Product");
        }

        #region Filter
        [HttpPost]
        public RedirectToActionResult AddCategoryToFilter(int categoryId)
        {
            Category category = CheckCategory(categoryId);

            if (category != null)
            {
                _sessionCategories.AddCategory(category);
            }

            return RedirectToAction("List", "Product", new { pageNow = 1 });
        }

        [HttpPost]
        public RedirectToActionResult DeleteCategoryToFilter(int categoryId)
        {
            Category category = CheckCategory(categoryId);
            
            _sessionCategories.RemoveCategory(category);

            return RedirectToAction("List", "Product", new { pageNow = 1 });
        }

        [HttpPost]
        public RedirectToActionResult ClearFilter()
        {
            _sessionCategories.Clear();

            return RedirectToAction("List", "Product");
        }

        #endregion

        public Category CheckCategory(int categoryId)
        {
            return _categoryRepository.GetCategories().FirstOrDefault(c => c.CategoryID == categoryId);
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
