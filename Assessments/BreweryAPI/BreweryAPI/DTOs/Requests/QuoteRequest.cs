namespace BreweryAPI.DTOs.Requests;

public class QuoteRequest
{
    public int WholesalerId { get; set; }
    private List<QuoteDetailRequest> Quotes { get; set; } = [];
}