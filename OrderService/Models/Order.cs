namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
