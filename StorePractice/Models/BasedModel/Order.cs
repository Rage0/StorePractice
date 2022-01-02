using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public ICollection<CartLine> Lines { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Line1 { get; set; }
        [Required]
        public string Line2 { get; set; }
        [Required]
        public string Line3 { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Zip { get; set; }
        public string OwnerId { get; set; }
    }
}
