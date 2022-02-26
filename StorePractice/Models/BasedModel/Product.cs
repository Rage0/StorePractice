using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.SqlModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace StorePractice.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 200000, ErrorMessage = "The price cannot be negative or equal zero")]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "The quantity cannot be negative or equal zero")]
        public int Quantity { get; set; } = 1;
        [StringLength(50, MinimumLength = 2, ErrorMessage = "String length must be between 2 and 50 characters")]
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public List<Category> Categories { get; set; }
        public string OwnerId { get; set; }

        public bool Discount { get; set; }
    }
}
