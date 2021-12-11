using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace StorePractice.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public ViewResult CreateRole() => View();

        public async Task<ViewResult> EditRole(string roleId)
        {
            return View(await _roleManager.FindByIdAsync(roleId));
        }

    }
}
