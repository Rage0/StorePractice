using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.ViewModels;
using System.Threading.Tasks;
using System.Security.Claims;

namespace StorePractice.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                return View(user);
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
