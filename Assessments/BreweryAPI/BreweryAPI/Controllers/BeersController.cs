namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeersController (IBeerRepository beerRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ListResponse<Beer>> GetBeers()
    {
        var beers = await beerRepository.GetAllBeers();

        return new()
        {
            Data = beers
        };
    }

    [HttpPost]
    public async Task<ActionResult<Beer>> AddBeer(BeerRequest beer)
    {
        var result = await beerRepository.AddAsync(new()
        {
            Name = beer.Name,
            BreweryId = beer.BreweryId,
            RetailPrice = beer.RetailPrice,
            WholesalePrice = beer.WholesalePrice
        });

        return result;
    }

    [HttpDelete("/{beerId}")]
    public async Task<ActionResult<Beer>> DeleteBeer(int beerId)
    {
        var result = await beerRepository.DeleteBeerById(beerId);

        return result;
    }

    [HttpPut("/{beerId}")]
    public async Task<ActionResult<Beer>> UpdateBeer(BeerRequest beer, int beerId)
    {
        return new Beer();
    }
}
