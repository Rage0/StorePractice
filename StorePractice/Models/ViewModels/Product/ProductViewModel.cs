using System.Collections.Generic;

namespace StorePractice.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public List<Category> HasCategories { get; set; }
        public List<Category> HasNotCategories { get; set; }
    }
}
