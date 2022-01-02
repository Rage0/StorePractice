using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace StorePractice.Models.ViewModels
{
    public class RoleViewModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}
