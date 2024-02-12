namespace BreweryAPI.Models;

public class InventoryItem
{
    public int Id { get; set; }
    public int WholesalerId { get; set; }
    public Wholesaler Wholesaler { get; set; }
    public int BeerId { get; set; }
    public Beer Beer { get; set; }
    public int Amount { get; set; }
}
