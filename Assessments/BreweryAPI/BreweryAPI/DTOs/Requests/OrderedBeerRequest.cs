namespace BreweryAPI.DTOs.Requests;

public class OrderedBeerRequest
{
    [Required]
    public int? BeerId { get; set; }
    [Required]
    public int? Amount { get; set; }
}
