using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Infrastructure
{
    public class ItemForTagOfPage
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string NameTag { get; set; }
        public int Page { get; set; }
        public bool HasCss { get; set; } = true;
    }
}
