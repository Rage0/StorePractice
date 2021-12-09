using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class Page
    {
        public int TotalItem { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItem / PageSize);
    }
}
