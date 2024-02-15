namespace BreweryAPI.Services;

public interface IPlaceOrderService
{
    Task<OrderResponse> PlaceWholesalerOrder(WholesalerBuyOrderRequest buyOrder);
}
