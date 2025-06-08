using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using System.Text.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<Order> _orders = new();
        private readonly HttpClient _httpClient;

        public OrdersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductService");
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            // Appeler ProductService pour récupérer les infos du produit
            var response = await _httpClient.GetAsync($"/api/products");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Erreur de communication avec ProductService");

            var json = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var product = products?.FirstOrDefault(p => p.Id == order.ProductId);

            if (product == null)
                return NotFound($"Produit {order.ProductId} introuvable");

            order.Id = _orders.Count + 1;
            order.Product = product;
            _orders.Add(order);

            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders() => Ok(_orders);
    }
}
