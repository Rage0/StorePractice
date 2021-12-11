using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace StorePractice.Controllers
{
    public class CrudRoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;

        public CrudRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
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
        public async Task<IActionResult> Edit(string id, IdentityRole roleEdit)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await _roleManager.FindByIdAsync(id);
                if (role != null)
                {
                    role.Name = roleEdit.Name;

                    IdentityResult result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Roles", "Admin");
                    }
                }
            }
            return RedirectToAction("EditRole", "Role", id);
        }
    }
}
