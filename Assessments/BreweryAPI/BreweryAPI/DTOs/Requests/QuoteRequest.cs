namespace BreweryAPI.DTOs.Requests;

public class QuoteRequest
{
    [Required]
    public int? WholesalerId { get; set; }
    [Required]
    public List<QuoteDetailRequest>? QuoteDetails { get; set; } = [];
}