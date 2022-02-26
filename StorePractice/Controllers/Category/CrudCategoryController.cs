using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StorePractice.Controllers
{
    [Authorize]
    public class CrudCategoryController : Controller
    {
        private EfCategoryRepository _categoryRepository;
        private UserManager<User> _userManager;
        public CrudCategoryController(EfCategoryRepository categories, UserManager<User> userManager)
        {
            _categoryRepository = categories;
            _userManager = userManager;
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
        public RedirectResult Edit(Category category, int categoryId, string returnUrl)
        {
            _categoryRepository.UpdateCategory(category, categoryId);

            return Redirect(returnUrl);
        }

        [HttpPost]
        public async Task<RedirectResult> Create(Category category, string returnUrl)
        {
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            User user = await _userManager.FindByIdAsync(currentUserId);
            if (user != null)
            {
                category.OwnerId = currentUserId;
            }
            
            _categoryRepository.CreateCategory(category);

            return Redirect(returnUrl);
        }
    }
}
