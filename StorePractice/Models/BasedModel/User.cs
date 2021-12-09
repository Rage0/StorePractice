using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorePractice.Models
{
    public class User : IdentityUser
    {
        public List<Product> HasProducts { get; set; }
        public List<Category> HasCategories { get; set; }
        public List<Order> HasOrders { get; set; }

    }
}
