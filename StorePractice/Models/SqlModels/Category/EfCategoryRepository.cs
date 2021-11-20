using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorePractice.Models.SqlModels
{
    public class EfCategoryRepository
    {
        private ApplicationsContext _repository;

        public EfCategoryRepository(ApplicationsContext repo)
        {
            _repository = repo;
        }

        public IQueryable<Category> GetCategories() => _repository.Categories;

        public void RemoveCategory(Category category)
        {
            _repository.Categories.Remove(category);
            _repository.SaveChanges();
        }

        public void CreateCategory(Category category)
        {
            _repository.Categories.Add(category);
            _repository.SaveChanges();
        }

        public void UpdateCategory(Category category, int id)
        {
            Category categoryForEdit = _repository.Categories.Find(id);

            categoryForEdit.Name = category.Name;
            _repository.SaveChanges();
        }
    }
}
