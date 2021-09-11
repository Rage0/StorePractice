using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorePractice.Models.SqlModels
{
    public class EfProductRepository : IProductRepository
    {
        private ApplicationsContext _repository;
        public EfProductRepository(ApplicationsContext repo)
        {
            _repository = repo;
        }

        public void AddProduct(Product product)
        {
            _repository.Attach(product.Categories);
            _repository.Products.Add(product);
            _repository.SaveChanges();
        }

        public IQueryable<Product> GetProducts() => _repository.Products
            .Include(o => o.Categories);
    }

}
