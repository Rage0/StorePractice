using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class Category
    {
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public List<Product> HasProducts { get; set; }
    }
}
