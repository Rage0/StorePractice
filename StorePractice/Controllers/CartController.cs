using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using StorePractice.Infrastructure;

namespace StorePractice.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository productRepository;
        private Cart cart;
        public CartController(IProductRepository repo, Cart cartProduct)
        {
            productRepository = repo;
            cart = cartProduct;
        }

        public ViewResult CartProduct() => View(cart);

        public RedirectToActionResult AddToCart(int productId)
        {
            Product product = productRepository.GetProducts().Where(p => p.ProductID == productId).FirstOrDefault();
            if (product != null)
            {
                cart.AddItem(product);
            }

            return RedirectToAction("CartProduct");
        }
        
    }
}
