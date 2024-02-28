namespace BreweryAPI.DTOs.Requests;

public class BeerRequest
{
    [Required]
    public int? BreweryId { get; set; }
    [Required]
    public string? Name { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
}
