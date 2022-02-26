using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.ViewModels;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;
using StorePractice.Models.SqlModels;
using System.Linq;

namespace StorePractice.Controllers
{
    
    public class CrudUserController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private EfProductRepository _productRepository;
        private EfCategoryRepository _categoryRepository;
        private EfOrderRepository _orderRepository;
        private IUserValidator<User> _userValidator;
        private IPasswordValidator<User> _passwordValidator;
        private IPasswordHasher<User> _passwordHasher;
        public CrudUserController(UserManager<User> users,
            RoleManager<IdentityRole> roleManager,
            IUserValidator<User> userValidator,
            IPasswordValidator<User> passwordValidator,
            IPasswordHasher<User> passwordHasher,
            EfProductRepository productRepository,
            EfCategoryRepository categoryRepository,
            EfOrderRepository orderRepository)
        {
            _userManager = users;
            _roleManager = roleManager;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName = userModel.Name,
                    Email = userModel.Email,
                };
            
                IdentityResult result = await _userManager.CreateAsync(user, userModel.Password);

                if (result.Succeeded)
                {
                    if (await _roleManager.FindByNameAsync("User") != null)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return RedirectToAction("Users", "Admin");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserViewModel userModel)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.UserName = userModel.Name;
                user.Email = userModel.Email;

                IdentityResult validUser = await _userValidator.ValidateAsync(_userManager, user);
                if (!validUser.Succeeded)
                {
                    return RedirectToAction("EditUser", "User", new { userId = userModel.Id });
                }

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(userModel.Password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, userModel.Password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, userModel.Password);
                    }
                    else
                    {
                        RedirectToAction("EditUser", "User", new { userId = userModel.Id });
                    }
                }

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users", "Admin");
                }
                else
                {
                    return RedirectToAction("EditUser", "User", new { userId = userModel.Id });
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var products = _productRepository.GetProducts().Where(p => p.OwnerId == user.Id).ToList();
                var orders = _orderRepository.GetOrders().Where(p => p.OwnerId == user.Id).ToList();
                var categories = _categoryRepository.GetCategories().Where(p => p.OwnerId == user.Id).ToList();

                _productRepository.RemoveProduct(products);
                _categoryRepository.RemoveCategory(categories);
                _orderRepository.RemoveOrder(orders);

                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users", "Admin");
        }
    }
}
