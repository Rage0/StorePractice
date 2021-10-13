using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorePractice.Models
{
    public class ProductInteraction
    {
        private Product _product;

        public virtual void AddProduct(Product product)
        {
            _product = product;
        }

        public virtual void AddCategoryToProduct(Category category)
        {
            if (_product != null)
            {
                _product.Categories.Add(category);
            }
        }

        public virtual void RemoveCategoryToProduct(Category category)
        {
            if (_product != null && _product.Categories.Contains(category))
            {
                _product.Categories.Remove(category);
            }
        }

        public virtual void ClearCategoriesToProduct()
        {
            if (_product != null)
            {
                _product.Categories.Clear();
            }
        }

    }
}
