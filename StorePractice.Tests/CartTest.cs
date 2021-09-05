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
    public class AddProductTest
    {
        [Fact]
        public void Can_CRUD_Cart()
        {
            List<Product> products = new List<Product>()
            {
                new Product { Name = "P1" , Price = 200.53M, ProductID = 1},
                new Product { Name = "P2" , Price = 100.10M, ProductID = 2},
                new Product { Name = "P3" , Price = 20, ProductID = 3},
            };


            Cart cart = new Cart();

            cart.AddItem(products[0]);
            cart.AddItem(products[0]);
            cart.AddItem(products[0]);
            cart.AddItem(products[1]);

            cart.RemoveItem(products[0]);
            cart.RemoveItem(products[1]);


            Assert.Equal(2, cart.GetItem.ElementAt(0).Quantity);
            Assert.Single(cart.GetItem);
        }
    }
}
