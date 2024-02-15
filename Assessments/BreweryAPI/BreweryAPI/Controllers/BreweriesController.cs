namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BreweriesController(IRepository<Brewery> breweryRepository, IBeerRepository beerRepository, IPlaceOrderService placeOrderService, IOrderService orderService) : ControllerBase
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

    [HttpPost("orders")]
    public async Task<ActionResult<OrderResponse>> CreateBuyOrder(WholesalerBuyOrderRequest buyOrder)
    {
        return Ok(await placeOrderService.PlaceWholesalerOrder(buyOrder));
    }

    [HttpGet("orders")]
    public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
    {
        return Ok(await orderService.GetOrderResponsesFromAllOrders());
    }
}
