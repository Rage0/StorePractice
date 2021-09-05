using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class LineCategories
    {
        private List<Category> categories = new List<Category>();
        public virtual List<Category> Categories => categories;

        public virtual void AddCategory(Category category)
        {
            Category HasCategory = categories
                .Where(c => c.CategoryID == category.CategoryID)
                .FirstOrDefault();

            if (HasCategory == null)
            {
                categories.Add(category);
            }
            
        }

        public virtual void RemoveCategory(Category category)
        {
            categories.RemoveAll(c => c.CategoryID == category.CategoryID);
        }

        public virtual void Clear()
        {
            categories.Clear();
        }

    }
}
