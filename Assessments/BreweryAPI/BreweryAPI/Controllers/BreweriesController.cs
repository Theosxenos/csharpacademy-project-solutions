namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreweriesController(IRepository<Brewery> repository, IBeerRepository beerRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ListResponse<Brewery>> GetAllBreweries()
    {
        return new()
        {
            Data = await repository.GetAll().ToListAsync()
        };
    }

    [HttpGet("/{breweryId}/Beers")]
    public async Task<ListResponse<Beer>> GetAllBeersForBreweryById(int breweryId)
    {
        return new()
        {
            Data = await beerRepository.GetBeersByBreweryId(breweryId)
        };
    }
}
