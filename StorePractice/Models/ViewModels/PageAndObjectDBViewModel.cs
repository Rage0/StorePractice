using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StorePractice.Models;

namespace StorePractice.Models.ViewModels
{
    public class PageAndObjectDBViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public Page Pages { get; set; }
        public LineCategories SessionCategories { get; set; }
        public int CurrentPage { get; set; }
        public string ActionUrl { get; set; }
    }
}
