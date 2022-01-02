using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using StorePractice.Models.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StorePractice.Controllers
{
    [Authorize]
    public class CrudProductController : Controller
    {
        private EfProductRepository _productRepository;
        private UserManager<User> _userManager;

        public CrudProductController(EfProductRepository products, UserManager<User> userManager)
        {
            _productRepository = products;
            _userManager = userManager;
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Product product = _productRepository.GetProducts().FirstOrDefault(p => p.ProductID == id);

            if (product != null)
            {
                _productRepository.RemoveProduct(product);
            }

            return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public RedirectResult Edit(ProductModificationViewModel product, int productId, string returnUrl)
        {
            product.ProductCategories = _productRepository
                .GetProducts()
                .FirstOrDefault(p => p.ProductID == productId)
                .Categories;

            _productRepository.UpdateProduct(product, productId);
            
            return Redirect(returnUrl);
        }

        [HttpPost]
        public IActionResult Create(ProductModificationViewModel product, string returnUrl)
        {
            string currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            product.UserId = currentUserId;

            _productRepository.CreateProduct(product);

            return Redirect(returnUrl);
        }
    }
}
