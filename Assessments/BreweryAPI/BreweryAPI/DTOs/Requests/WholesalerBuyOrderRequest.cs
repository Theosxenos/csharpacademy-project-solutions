namespace BreweryAPI.DTOs.Requests;

public class WholesalerBuyOrderRequest
{
    public int WholesalerId { get; set; }
    public int BreweryId { get; set; }
    public List<OrderedBeerRequest> OrderedBeers { get; set; } = [];
}
