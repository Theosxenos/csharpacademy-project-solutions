namespace BreweryAPI.DTOs.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public Wholesaler? Wholesaler { get; set; }
    public Brewery? Brewery { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime? CreatedAt { get; set; }
    public List<OrderDetailResponse> OrderDetails { get; set; } = [];
}
