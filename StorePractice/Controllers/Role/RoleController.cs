using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models.ViewModels;
using StorePractice.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using StorePractice.Infrastructure;

namespace StorePractice.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public ViewResult CreateRole() => View();

        public async Task<IActionResult> EditRole(string roleId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);
            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();
            
            if (role != null)
            {
                members.AddRange(await role.GetMembersToRoleAsync(_userManager));
                nonMembers.AddRange(await role.GetNonMembersToRoleAsync(_userManager));
            }
            

            return View(new RoleViewModel
            {
                Role = role,

                Members = members,

                NonMembers = nonMembers
            });
        }


    }
}
