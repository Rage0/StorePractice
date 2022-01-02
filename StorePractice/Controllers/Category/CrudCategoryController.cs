using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using System.Linq;
using System.Security.Claims;

namespace StorePractice.Controllers
{
    [Authorize]
    public class CrudCategoryController : Controller
    {
        private EfCategoryRepository _categoryRepository;
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
            category.OwnerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _categoryRepository.CreateCategory(category);

            return RedirectToAction("Category", "Admin");
        }
    }
}
