using Microsoft.AspNetCore.Mvc;
using StorePractice.Models;
using StorePractice.Models.SqlModels;
using System.Linq;

namespace StorePractice.Controllers
{
    public class CrudProductController : Controller
    {
        EfProductRepository _productRepository;

        public CrudProductController(EfProductRepository products)
        {
            _productRepository = products;
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
        public RedirectToActionResult Edit(Product product, int productId)
        {
            _productRepository.UpdateProduct(product, productId);

            return RedirectToAction("Product", "Admin");
        }

        [HttpPost]
        public RedirectToActionResult Create(Product product)
        {
            _productRepository.CreateProduct(product);

            return RedirectToAction("Product", "Admin");
        }
    }
}
