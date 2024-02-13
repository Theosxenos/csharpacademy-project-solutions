namespace BreweryAPI.Models;

public class Beer
{
    public int Id { get; set; }
    public int BreweryId { get; set; }
    public Brewery Brewery { get; set; }
    public string Name { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal WholesalePrice { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal RetailPrice { get; set; }
}
