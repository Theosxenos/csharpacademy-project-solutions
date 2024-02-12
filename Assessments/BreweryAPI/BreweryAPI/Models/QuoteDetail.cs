namespace BreweryAPI.Models;

public class QuoteDetail
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public Beer Beer { get; set; }
    public int QuoteId { get; set; }
    public Quote Quote { get; set; }
    public int Amount { get; set; }
    public float Discount { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
}
