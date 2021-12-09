using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public User User { get; set; }
        public string OwnerId { get; set; }
    }
}
