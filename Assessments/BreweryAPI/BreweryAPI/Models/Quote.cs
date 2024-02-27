namespace BreweryAPI.Models;

public class Quote
{
    public int Id { get; set; }
    public int WholesalerId { get; set; }
    public Wholesaler Wholesaler { get; set; }
    public int Discount { get; set; }
    public List<QuoteDetail> QuoteDetails { get; set; } = [];
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
}
