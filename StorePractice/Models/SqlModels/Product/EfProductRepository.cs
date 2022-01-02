using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StorePractice.Models.ViewModels;

namespace StorePractice.Models.SqlModels
{
    public class EfProductRepository
    {
        private ApplicationsContext _repository;
        public EfProductRepository(ApplicationsContext repo)
        {
            _repository = repo;
        }

        public void CreateProduct(ProductModificationViewModel product)
        {
            List<Category> categories = new List<Category>();

            foreach (int categoryId in product.ToAdd ?? new int[] {})
            {
                Category category = _repository.Categories.FirstOrDefault(c => c.CategoryID == categoryId);

                if (category != null)
                {
                    categories.Add(category);
                }
            }

            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Categories = categories,
                OwnerId = product.UserId,
            };

            _repository.Products.Add(newProduct);
            _repository.SaveChanges();
        }

        public void UpdateProduct(ProductModificationViewModel product, int id)
        {
            Product productForEdit = _repository.Products.FirstOrDefault(p => p.ProductID == id);

            if (productForEdit != null)
            {
                productForEdit.Name = product.Name;
                productForEdit.Price = product.Price;
                productForEdit.Quantity = product.Quantity;
                productForEdit.Description = product.Description;

                foreach (int categoryId in product.ToAdd ?? new int[] { })
                {
                    Category category = _repository.Categories.FirstOrDefault(c => c.CategoryID == categoryId);

                    if (category != null && !product.ProductCategories.Contains(category))
                    {
                        product.ProductCategories.Add(category);
                    }
                }

                foreach (int categoryId in product.ToDelete ?? new int[] { })
                {
                    Category category = _repository.Categories.FirstOrDefault(c => c.CategoryID == categoryId);

                    if (category != null && product.ProductCategories.Contains(category))
                    {
                        product.ProductCategories.Remove(category);
                    }
                }

                productForEdit.Categories = product.ProductCategories;

                _repository.SaveChanges();
            }
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
