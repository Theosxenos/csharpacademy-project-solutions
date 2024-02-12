namespace BreweryAPI.Models;

public class Wholesaler
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<InventoryItem> Inventory { get; set; }
    public List<Order> Orders { get; set; }
    public List<Quote> Quotes { get; set; }
}
