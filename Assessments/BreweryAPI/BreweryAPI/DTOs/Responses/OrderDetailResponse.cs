namespace BreweryAPI.DTOs.Responses;

public class OrderDetailResponse
{
    public int Id { get; set; }
    public BeerResponse Beer { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
