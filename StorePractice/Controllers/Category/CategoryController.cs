using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using StorePractice.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace StorePractice.Controllers
{
    public class CategoryController : Controller
    {
        private EfCategoryRepository _categoryRepository;
        private List<string> _charCategories = new List<string>();

        public CategoryController(EfCategoryRepository repo)
        {
            _categoryRepository = repo;
        }

        [AllowAnonymous]
        public ViewResult Categories(string charFilter = "A")
        {
            return View(new CategoryFilterViewModel
            {
                Categories = _categoryRepository.GetCategories()
                             .Where(f => f.Name.StartsWith(charFilter)),

                CharsCategories = CharCategories()
            });
        }

        public ViewResult EditOrCreate(int categoryId, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
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
