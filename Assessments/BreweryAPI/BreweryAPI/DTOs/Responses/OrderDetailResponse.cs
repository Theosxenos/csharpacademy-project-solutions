namespace BreweryAPI.DTOs.Responses;

public class OrderDetailResponse
{
    public int Id { get; set; }
    public Beer Beer { get; set; }
    public int Amount { get; set; }
    public float Discount { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
