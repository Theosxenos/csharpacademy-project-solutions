namespace BreweryAPI.Services;

public class OrderService(IOrderRepository repository, IMapper mapper) : IOrderService
{
    public async Task<List<OrderResponse>> GetOrderResponsesFromAllOrders()
    {
        var orders = await repository.GetAll().ToListAsync();
        return mapper.Map<List<OrderResponse>>(orders);
    }
}
