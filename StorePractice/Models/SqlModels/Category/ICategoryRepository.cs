using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models.SqlModels
{
    public interface ICategoryRepository
    {
        public IQueryable<Category> GetCategories();
        public void CreateCategory(Category category);
        public void RemoveCategory(Category category);
        public void UpdateCategory(Category category, int id);
    }
}
