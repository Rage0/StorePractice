using StorePractice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StorePractice.Infrastructure
{
    public static class LinqExtension
    {
        public static IEnumerable<Product> SortByArray
            (this IEnumerable<Product> method, IEnumerable<Category> input)
        {
            var products = method.GetEnumerator();

            foreach (Category category in input)
            {
                Console.WriteLine($"Categories: {category.Name}");
            }
            
            while (products.MoveNext())
            {
                if (SortCollectionCategory(products.Current, input.ToList()))
                {
                    yield return products.Current;
                }
            }
        }

        private static bool SortCollectionCategory(Product product, List<Category> categories)
        {
            var productCategories = product.Category.GetEnumerator();

            while (productCategories.MoveNext())
            {
                Console.WriteLine($"product: {product.Name}");
                foreach (Category category in categories)
                {
                    Console.WriteLine($"Equals: {productCategories.Current.Name == category.Name}");
                    Console.WriteLine($"Category product: {productCategories.Current.Name}; Category: {category.Name}\n");
                    if (productCategories.Current.Name == category.Name)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}
