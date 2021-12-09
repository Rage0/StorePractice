using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.ViewModels;
using System.Threading.Tasks;
using System;

namespace StorePractice.Controllers
{
    public class CrudUserController : Controller
    {
        private readonly UserManager<User> _userManager;
        public CrudUserController(UserManager<User> users)
        {
            _userManager = users;
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

        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.UserName = userModel.Name;
                    user.Email = userModel.Email;
                    user.PasswordHash = userModel.Password;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
            return RedirectToAction("Users", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
               await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Users", "Admin");
        }
    }
}
