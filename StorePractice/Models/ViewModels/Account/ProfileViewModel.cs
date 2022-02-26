using System.Collections.Generic;

namespace StorePractice.Models.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }
    }
}
