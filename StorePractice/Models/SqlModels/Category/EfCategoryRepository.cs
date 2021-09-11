using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorePractice.Models.SqlModels
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private ApplicationsContext _repository;

        public EfCategoryRepository(ApplicationsContext repo)
        {
            _repository = repo;
        }

        public IQueryable<Category> GetCategories() => _repository.Categories;

        public void SaveCategory(Category category)
        {
            _repository.Categories.Add(category);
            _repository.SaveChanges();
        }

    }
}
