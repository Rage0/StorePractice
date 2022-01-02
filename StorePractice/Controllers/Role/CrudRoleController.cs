using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StorePractice.Controllers
{
    public class CrudRoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public CrudRoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create([Required]string roleName)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Admin");
                }
            }
            return RedirectToAction("CreateRole", "Role", roleName);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction("Roles", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleModification roleModification)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    role.Name = roleModification.RoleName;

                    foreach (string userId in roleModification.ToAdd ?? new string[] { })
                    {
                        User user = await _userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            result = await _userManager.AddToRoleAsync(user, roleModification.RoleName);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Roles", "Admin");
                            }
                        }
                    }

                    foreach (string userId in roleModification.ToDelete ?? new string[] { })
                    {
                        User user = await _userManager.FindByIdAsync(userId);
                        if (user != null)
                        {
                            result = await _userManager.RemoveFromRoleAsync(user, roleModification.RoleName);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Roles", "Admin");
                            }
                        }
                    }

                    await _roleManager.UpdateAsync(role);

                }
                return RedirectToAction("Roles", "Admin");
            }
            return RedirectToAction("EditRole", "Role", id);
        }
    }
}
