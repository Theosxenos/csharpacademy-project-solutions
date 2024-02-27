namespace BreweryAPI.Interfaces.Services;

public interface IPlaceOrderService
{
    Task<OrderResponse> PlaceWholesalerOrder(WholesalerBuyOrderRequest buyOrder);
}
