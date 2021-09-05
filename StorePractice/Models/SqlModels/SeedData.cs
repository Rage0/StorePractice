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
                repository.SaveChanges();
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
                        Category = new List<Category>() {},
                    },
                    new Product
                    {
                        Name = "Shoes",
                        Price = 40.23M,
                        Category = new List<Category>() { categories.ElementAt(0) },
                    },
                    new Product
                    {
                        Name = "Short",
                        Price = 50.23M,
                        Category = new List<Category>() { categories.ElementAt(0) }
                    },
                    new Product
                    {
                        Name = "Jacked",
                        Price = 60.23M,
                        Category = new List<Category>() { categories.ElementAt(0) },
                        Discount = true
                    },
                    new Product
                    {
                        Name = "Socks",
                        Price = 10.23M,
                        Category = new List<Category>() { categories.ElementAt(0) }
                    },
                    new Product { Name = "Ring", Price = 120.23M },
                    new Product
                    {
                        Name = "Axe",
                        Price = 310.23M,
                        Category = new List<Category>() {}
                    },
                    new Product { Name = "Toy pistol", Price = 70.23M, Discount = true },
                    new Product
                    {
                        Name = "Knife",
                        Price = 290.23M,
                        Category = new List<Category>() {}
                    },
                    new Product { Name = "Package", Price = 5.23M },
                    new Product
                    {
                        Name = "Gold Carrot",
                        Price = 10000.23M,
                        Category = new List<Category>() {},
                    },
                    new Product
                    {
                        Name = "Hat",
                        Price = 120.23M,
                        Category = new List<Category>() {}
                    },
                    new Product { Name = "Book", Price = 285.23M },
                    new Product { Name = "Glass", Price = 110.23M, Discount = true },
                    new Product
                    {
                        Name = "Lib liner",
                        Price = 255.23M,
                        Category = new List<Category>() {}
                    },
                    new Product { Name = "Instruments", Price = 520.23M },
                    new Product
                    {
                        Name = "Cup",
                        Price = 55.23M,
                        Category = new List<Category>() {}
                    },
                    new Product
                    {
                        Name = "Flower",
                        Price = 47.23M,
                        Category = new List<Category>() {}
                    }
                );
                repository.SaveChanges();
            }

            
                
        }
    }
}
