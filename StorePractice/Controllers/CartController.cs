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
        private EfProductRepository _productRepository;
        private Cart _sessionCart;
        public CartController(EfProductRepository repo, Cart cartProduct)
        {
            _productRepository = repo;
            _sessionCart = cartProduct;
        }

        public ViewResult CartProduct() => View(_sessionCart);

        public RedirectToActionResult AddToCart(int productId)
        {
            Product product = _productRepository.GetProducts().Where(p => p.ProductID == productId).FirstOrDefault();
            if (product != null)
            {
                _sessionCart.AddItem(product);
            }

            return RedirectToAction("CartProduct");
        }

        public RedirectToActionResult ClearCart()
        {
            _sessionCart.Clear();
            return RedirectToAction("List", "Product");
        }
    }
}
