using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class LineCategories
    {
        private List<Category> _categories = new List<Category>();
        public virtual List<Category> Categories => _categories;

        public virtual void AddCategory(Category category)
        {
            Category HasCategory = _categories
                .Where(c => c.CategoryID == category.CategoryID)
                .FirstOrDefault();

            if (HasCategory == null)
            {
                _categories.Add(category);
            }
            
        }

        public virtual void RemoveCategory(Category category)
        {
            _categories.RemoveAll(c => c.CategoryID == category.CategoryID);
        }

        public virtual void Clear()
        {
            _categories.Clear();
        }

    }
}
