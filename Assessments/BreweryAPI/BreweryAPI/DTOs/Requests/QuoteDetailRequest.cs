namespace BreweryAPI.DTOs.Requests;

public class QuoteDetailRequest
{
    [Required]
    public int? BeerId { get; set; }
    [Required]
    public int? Amount { get; set; }
}