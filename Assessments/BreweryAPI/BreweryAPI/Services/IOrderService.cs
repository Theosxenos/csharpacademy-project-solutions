namespace BreweryAPI.Services;

public interface IOrderService
{
    Task<OrderResponse> PlaceWholesalerOrder(WholesalerBuyOrderRequest buyOrder);
}
