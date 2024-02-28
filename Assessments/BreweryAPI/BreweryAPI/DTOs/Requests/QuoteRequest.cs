namespace BreweryAPI.DTOs.Requests;

public class QuoteRequest
{
    public int WholesalerId { get; set; }
    public List<QuoteDetailRequest> QuoteDetails { get; set; } = [];
}