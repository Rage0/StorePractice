using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class Category
    {
        [Required]
        public string Name { get; set; }
        public int CategoryID { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [NotMapped]
        public List<Product> HasProducts { get; set; }

        public string OwnerId { get; set; }
    }
}
