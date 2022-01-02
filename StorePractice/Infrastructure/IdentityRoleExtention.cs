using Microsoft.AspNetCore.Identity;
using StorePractice.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Infrastructure
{
    public static class IdentityRoleExtention
    {
        public static async Task<List<User>> GetMembersToRoleAsync(this IdentityRole role, UserManager<User> userManager)
        {
            List<User> users = new List<User>();

            foreach (User user in userManager.Users.ToList())
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    users.Add(user);
                }
            }

            return users;
        }

        public static async Task<List<User>> GetNonMembersToRoleAsync(this IdentityRole role, UserManager<User> userManager)
        {
            List<User> users = new List<User>();

            foreach (User user in userManager.Users.ToList())
            {
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    users.Add(user);
                }
            }

            return users;
        }

    }

}
