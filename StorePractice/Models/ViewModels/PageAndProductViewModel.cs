using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models;

namespace StorePractice.Models.ViewModels
{
    public class PageAndProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public Page Pages { get; set; }
        public LineCategories CurrentCategories { get; set; }
        public int CurrentPage { get; set; }
    }
}
