using System.Collections;
using System.Collections.Generic;

namespace StorePractice.Models
{
    public class Role
    {
        public string Name { get; set; }

        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}
