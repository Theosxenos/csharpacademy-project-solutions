namespace BreweryAPI.Interfaces.Services;

public interface IOrderService
{
    Task<List<OrderResponse>> GetOrderResponsesFromAllOrders();
}