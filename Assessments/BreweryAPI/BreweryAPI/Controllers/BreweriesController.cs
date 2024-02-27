namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreweriesController(IRepository<Brewery> breweryRepository, IBeerRepository beerRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ListResponse<Brewery>> GetAllBreweries()
    {
        return new()
        {
            Data = await breweryRepository.GetAll().ToListAsync()
        };
    }

    [HttpGet("{breweryId}/beers")]
    public async Task<ListResponse<Beer>> GetAllBeersForBreweryById(int breweryId)
    {
        return new()
        {
            Data = await beerRepository.GetBeersByBreweryId(breweryId)
        };
    }
}
