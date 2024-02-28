namespace BreweryAPI.DTOs.Requests;

public class WholesalerBuyOrderRequest
{
    [Required]
    public int? WholesalerId { get; set; }
    [Required]
    public int? BreweryId { get; set; }
    [Required]
    public List<OrderedBeerRequest>? OrderedBeers { get; set; } = [];
}
