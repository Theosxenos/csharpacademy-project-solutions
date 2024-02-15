namespace BreweryAPI.Services;

public interface IOrderService
{
    Task<List<OrderResponse>> GetOrderResponsesFromAllOrders();
}