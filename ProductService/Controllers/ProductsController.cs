using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // En mémoire pour l'exemple
        private static List<Product> _products = new List<Product>();

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(_products);
        }

        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            product.Id = _products.Count + 1;
            _products.Add(product);
            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }
    }
}
