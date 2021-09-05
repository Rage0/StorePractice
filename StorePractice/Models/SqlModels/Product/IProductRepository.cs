using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.SqlModels;

namespace StorePractice.Models.SqlModels
{
    public interface IProductRepository
    {
        public IQueryable<Product> GetProducts();
        public void AddProduct(Product product);
    }
}
