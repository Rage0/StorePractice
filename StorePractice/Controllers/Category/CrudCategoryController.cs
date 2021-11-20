using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using System.Linq;

namespace StorePractice.Controllers
{
    public class CrudCategoryController : Controller
    {
        public EfCategoryRepository _categoryRepository;
        public CrudCategoryController(EfCategoryRepository categories)
        {
            _categoryRepository = categories;
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
    }
}
