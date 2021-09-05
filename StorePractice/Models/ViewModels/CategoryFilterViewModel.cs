using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models.ViewModels
{
    public class CategoryFilterViewModel
    {
        public List<string> CharsCategories { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
