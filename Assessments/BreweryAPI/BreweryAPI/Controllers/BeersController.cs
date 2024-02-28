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

    [HttpGet("{beerId:int}")]
    public async Task<ActionResult<Beer>> GetBeerById(int beerId)
    {
        return await beerRepository.GetBeerById(beerId);
    }

    [HttpPost]
    public async Task<ActionResult<Beer>> AddBeer(BeerRequest beer)
    {
        var result = await beerRepository.AddAsync(new()
        {
            Name = beer.Name,
            BreweryId = beer.BreweryId.Value,
            RetailPrice = beer.RetailPrice,
            WholesalePrice = beer.WholesalePrice
        });

        return CreatedAtAction(nameof(GetBeerById), new{beerId = result.Id}, result);
    }

    [HttpDelete("{beerId:int}")]
    public async Task<ActionResult<Beer>> DeleteBeer(int beerId)
    {
        var result = await beerRepository.DeleteBeerById(beerId);

        return result;
    }

    [HttpPut("{beerId:int}")]
    public async Task<ActionResult<Beer>> UpdateBeer(BeerRequest beer, int beerId)
    {
        var result = await beerRepository.UpdateBeer(beer, beerId);

        return result;
    }
}
