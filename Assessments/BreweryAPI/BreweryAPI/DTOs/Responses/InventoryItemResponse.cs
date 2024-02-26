namespace BreweryAPI.DTOs.Responses;

public class InventoryItemResponse
{
    public int Id { get; set; }
    public Wholesaler Wholesaler { get; set; }
    public BeerResponse Beer { get; set; }
    public int Amount { get; set; }
}