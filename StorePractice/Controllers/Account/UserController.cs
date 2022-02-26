using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.ViewModels;
using System.Threading.Tasks;
using System.Security.Claims;
using StorePractice.Models.SqlModels;
using System.Linq;

namespace StorePractice.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private EfProductRepository _productRepository;
        private EfCategoryRepository _categoryRepository;
        private EfOrderRepository _orderRepository;
        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            EfProductRepository productRepository,
            EfOrderRepository orderRepository,
            EfCategoryRepository categoryRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _categoryRepository = categoryRepository;
        }

        [AllowAnonymous]
        public ViewResult CreateUser() => View(new UserViewModel());
        public async Task<IActionResult> EditUser(string userId)
        {
           User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                UserViewModel userModel = new UserViewModel()
                {
                    Name = user.UserName,
                    Email = user.Email,
                    Id = user.Id,
                };
                
                return View(userModel);
            }

            return RedirectToAction("Users", "Admin");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserId);
            if (user != null)
            {
                return View(new ProfileViewModel
                {
                    User = user,

                    Products = _productRepository.GetProducts().Where(p => p.OwnerId == user.Id).ToList(),

                    Categories = _categoryRepository.GetCategories().Where(c => c.OwnerId == user.Id).ToList(),

                    Orders = _orderRepository.GetOrders().Where(o => o.OwnerId == user.Id).ToList(),
                });
            }

            return Redirect(nameof(Login));
        }

        [AllowAnonymous]
        public ViewResult Login()
        {
            _signInManager.SignOutAsync();
            return View(new LoginViewModel());
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("List", "Product");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(loginUser.Email);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager
                        .PasswordSignInAsync(user, loginUser.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Redirect(returnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(loginUser.Email), "Invalid email or password");
            }
            return View(loginUser);
        }
    }
}
