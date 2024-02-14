namespace BreweryAPI.DTOs.Requests;

public class OrderedBeerRequest
{
    public int BeerId { get; set; }
    public int Amount { get; set; }
}
