namespace BreweryAPI.DTOs.Requests;

public class BeerRequest
{
    public int BreweryId { get; set; }
    public string Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal WholesalePrice { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal RetailPrice { get; set; }
}
