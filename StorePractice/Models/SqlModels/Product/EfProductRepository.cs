using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StorePractice.Models.SqlModels
{
    public class EfProductRepository : IProductRepository
    {
        private ApplicationsContext repository;
        public EfProductRepository(ApplicationsContext repo)
        {
            repository = repo;
        }

        public void AddProduct(Product product)
        {
            repository.Attach(product.Categories);
            repository.Products.Add(product);
            repository.SaveChanges();
        }

        public IQueryable<Product> GetProducts() => repository.Products
            .Include(o => o.Categories);
    }

}
