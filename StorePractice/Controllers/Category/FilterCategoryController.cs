using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using System.Linq;

namespace StorePractice.Controllers
{
    public class FilterCategoryController : Controller
    {
        EfCategoryRepository _categoryRepository;
        LineCategories _sessionCategories;

        public FilterCategoryController(EfCategoryRepository categories, LineCategories sessionCategories)
        {
            _categoryRepository = categories;
            _sessionCategories = sessionCategories;
        }

        [HttpPost]
        public RedirectToActionResult AddCategoryToFilter(int categoryId)
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
    }
}
