using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace StorePractice.Models.SqlModels
{
    public static class SeedData
    {
        public static void AddData(IApplicationBuilder app)
        {
            ApplicationsContext repository = app.ApplicationServices.GetRequiredService<ApplicationsContext>();

            if (!repository.Categories.Any())
            {
                repository.Categories
                .AddRange
                (
                    new Category { Name = "Clothing" },
                    new Category { Name = "Toy" },
                    new Category { Name = "Instrument" },
                    new Category { Name = "More" },
                    new Category { Name = "Cosmetics" },
                    new Category { Name = "Accessory" },
                    new Category { Name = "Discount" }
                );

                for (int i = 0; i < 100; i++)
                {
                    repository.Categories.Add(
                        new Category()
                        {
                            Name = $"Category {i}"
                        }
                        );
                    repository.SaveChanges();
                }

                
            }

            var categories = repository.Categories.ToList();

            if (!repository.Products.Any())
            {
                repository.Products
                .AddRange
                (
                    new Product
                    {
                        Name = "Umbrella",
                        Price = 30.23M,
                        Categories = new List<Category>() { categories[1], categories[5] },
                    },
                    new Product
                    {
                        Name = "Shoes",
                        Price = 40.23M,
                        Categories = new List<Category>() { categories[0] },
                    },
                    new Product
                    {
                        Name = "Short",
                        Price = 50.23M,
                        Categories = new List<Category>() { categories[0] }
                    },
                    new Product
                    {
                        Name = "Jacked",
                        Price = 60.23M,
                        Categories = new List<Category>() { categories[0] },
                        Discount = true
                    },
                    new Product
                    {
                        Name = "Socks",
                        Price = 10.23M,
                        Categories = new List<Category>() { categories[0] }
                    },
                    new Product { Name = "Ring", Price = 120.23M },
                    new Product
                    {
                        Name = "Axe",
                        Price = 310.23M,
                        Categories = new List<Category>() { categories[2] }
                    },
                    new Product
                    {
                        Name = "Toy pistol",
                        Price = 70.23M,
                        Discount = true,
                        Categories = new List<Category>() { categories[1] }
                    },
                    new Product
                    {
                        Name = "Knife",
                        Price = 290.23M,
                        Categories = new List<Category>() { categories[2], categories[1] }
                    },
                    new Product { Name = "Package", Price = 5.23M },
                    new Product
                    {
                        Name = "Gold Carrot",
                        Price = 10000.23M,
                        Categories = new List<Category>() { categories[5] },
                    },
                    new Product
                    {
                        Name = "Hat",
                        Price = 120.23M,
                        Categories = new List<Category>() { categories[5], categories[0] }
                    },
                    new Product { Name = "Book", Price = 285.23M },
                    new Product { Name = "Glass", Price = 110.23M, Discount = true },
                    new Product
                    {
                        Name = "Lib liner",
                        Price = 255.23M,
                        Categories = new List<Category>() { categories[4] }
                    },
                    new Product { Name = "Instruments", Price = 520.23M },
                    new Product
                    {
                        Name = "Cup",
                        Price = 55.23M,
                        Categories = new List<Category>() { categories[3] }
                    },
                    new Product
                    {
                        Name = "Flower",
                        Price = 47.23M,
                        Categories = new List<Category>() { categories[3] }
                    }
                );

                for (int i = 0; i < 1000; i++)
                {
                    repository.Products.Add(new Product()
                    {
                        Name = $"Product {i}",
                        Price = 100,
                    });
                    repository.SaveChanges();
                }
                
            }

            
                
        }
    }
}
