using System;
using Xunit;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using StorePractice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace StorePractice.Tests
{
    public class AdminTest
    {
        [Fact]
        public void CrudOfAdmin()
        {
            Mock<IProductRepository> mockProduct = new Mock<IProductRepository>();
            mockProduct.Setup(m => m.GetProducts()).Returns((new Product[] { 
                new Product {Name = "Product1", Price = 100, ProductID = 1, Quantity = 2},
                new Product {Name = "Product2", Price = 200, ProductID = 2, Quantity = 5},
                new Product {Name = "Product3", Price = 300, ProductID = 3, Quantity = 1},
                new Product {Name = "Product4", Price = 400, ProductID = 4, Quantity = 10},
            }).AsQueryable());


            ProductController controller = new ProductController(mockProduct.Object, new LineCategories());

            controller.Create(new Product() { Name = "Product5", Price = 1000, ProductID = 5 });
            controller.Remove(1);

            Product productForEdit = mockProduct.Object.GetProducts().First(p => p.ProductID == 2);
            productForEdit.Name = "Product2 (Edit)";
            productForEdit.Price = 220;
            controller.Edit(productForEdit);

            List<Product> result = mockProduct.Object.GetProducts().ToList();
            /*Assert.True(result.Last().Name == "Product5");*/
            Assert.NotNull(result.FirstOrDefault(p => p.ProductID == 1));
            Assert.Equal("Product2 (Edit)", result[0].Name);
        }
    }
}
