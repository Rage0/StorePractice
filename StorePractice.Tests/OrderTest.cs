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
    
    public class OrderTest
    {
        [Fact]
        public void Can_Add_Order()
        {
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            mock.Setup(o => o.GetOrders()).Returns((new Order[] 
            { }
            ).AsQueryable<Order>());
            Cart cart = new Cart();


            OrderController target = new OrderController(mock.Object, cart);
            target.AddOrder(new Order() { OrderID = 0});


            Assert.Equal(1, mock.Object.GetOrders().Count());
        }
    }
}
