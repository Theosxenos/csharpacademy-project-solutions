namespace BreweryAPI.DTOs.Requests;

public class BeerRequest
{
    public int BreweryId { get; set; }
    public string Name { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
}
