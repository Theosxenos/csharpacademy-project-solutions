namespace BreweryAPI.Models;

public class Order
{
    public int Id { get; set; }
    public int WholesalerId { get; set; }
    public Wholesaler Wholesaler { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<OrderDetail> OrderDetails { get; set; } = [];
}
