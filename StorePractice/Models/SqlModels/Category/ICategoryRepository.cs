using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models.SqlModels
{
    public interface ICategoryRepository
    {
        public IQueryable<Category> GetCategories();
        public void SaveCategory(Category category);
    }
}
