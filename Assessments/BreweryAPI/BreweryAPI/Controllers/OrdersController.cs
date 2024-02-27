namespace BreweryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IPlaceOrderService placeOrderService, IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderResponse>> CreateBuyOrder(WholesalerBuyOrderRequest buyOrder)
    {
        return Ok(await placeOrderService.PlaceWholesalerOrder(buyOrder));
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderResponse>>> GetAllOrders()
    {
        return Ok(await orderService.GetOrderResponsesFromAllOrders());
    }
}