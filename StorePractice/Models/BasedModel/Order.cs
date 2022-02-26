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
        [Required(ErrorMessage = "Name is not correct")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Line 1 is not correct")]
        public string Line1 { get; set; }
        [Required(ErrorMessage = "Line 2 is not correct")]
        public string Line2 { get; set; }
        [Required(ErrorMessage = "Line 3 is not correct")]
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Country is not correct")]
        public string Country { get; set; }
        [Required(ErrorMessage = "City is not correct")]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip is not correct")]
        public string Zip { get; set; }
        public string OwnerId { get; set; }
    }
}
