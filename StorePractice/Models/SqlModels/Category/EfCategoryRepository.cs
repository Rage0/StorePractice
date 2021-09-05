using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorePractice.Models.SqlModels
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private ApplicationsContext repository;

        public EfCategoryRepository(ApplicationsContext repo)
        {
            repository = repo;
        }

        public IQueryable<Category> GetCategories() => repository.Categories;

        public void SaveCategory(Category category)
        {
            repository.Categories.Add(category);
            repository.SaveChanges();
        }

    }
}
