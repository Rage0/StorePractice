using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.SqlModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorePractice.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        public string Name { get; set; }
        public string Discription { get; set; }
        public List<Category> Categories { get; set; }

        public Product()
        {
            Categories = new List<Category>();
        }

        public bool Discount { get; set; }

    }
}
