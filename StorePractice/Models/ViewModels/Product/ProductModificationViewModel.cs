using System.Collections.Generic;

namespace StorePractice.Models.ViewModels
{
    public class ProductModificationViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
        public List<Category> ProductCategories { get; set; }
        public int[] ToAdd { get; set; }
        public int[] ToDelete { get; set; }
    }
}
