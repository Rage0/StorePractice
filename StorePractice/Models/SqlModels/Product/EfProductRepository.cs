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

        public void CreateProduct(Product product)
        {
            if (product.Categories == null)
            {
                product.Categories = new List<Category>();
            }
            else
            {
                _repository.Attach(product.Categories);
            }
            _repository.Products.Add(product);
            _repository.SaveChanges();
        }

        public void UpdateProduct(Product product, int id)
        {
            Product productForEdit = _repository.Products.Find(id);

            productForEdit.Name = product.Name;
            productForEdit.Price = product.Price;
            productForEdit.Quantity = product.Quantity;
            productForEdit.Discription = product.Discription;
            productForEdit.Discount = product.Discount;
            _repository.SaveChanges();
        }

        public IQueryable<Product> GetProducts() => _repository.Products
            .Include(o => o.Categories);

        public void RemoveProduct(Product product)
        {
            _repository.Products.Remove(product);
            _repository.SaveChanges();
        }
    }

}
