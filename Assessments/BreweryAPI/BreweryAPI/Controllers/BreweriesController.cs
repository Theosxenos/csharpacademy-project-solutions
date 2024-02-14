namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreweriesController(IRepository<Brewery> repository, IBeerRepository beerRepository, IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ListResponse<Brewery>> GetAllBreweries()
    {
        return new()
        {
            Data = await repository.GetAll().ToListAsync()
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

    [HttpPost("orders")]
    public async Task<ActionResult<OrderResponse>> CreateBuyOrder(WholesalerBuyOrderRequest buyOrder)
    {
        return Ok(await orderService.PlaceWholesalerOrder(buyOrder));
    }
}
