namespace BreweryAPI.Services;

public class OrderService(IRepository<Order> repository,IBeerRepository beerRepository, IMapper mapper) : IOrderService
{
    public async Task<OrderResponse> PlaceWholesalerOrder(WholesalerBuyOrderRequest buyOrder)
    {
        var order = mapper.Map<Order>(buyOrder);

        await CalculateBeerTotalPrice(order.OrderDetails);
        CalculateOrderTotalPrice(order);

        var result = await repository.AddAsync(order);

        return mapper.Map<OrderResponse>(result);;
    }

    private void CalculateOrderTotalPrice(Order order)
    {
        order.TotalPrice = order.OrderDetails.Sum(od => od.TotalPrice);
    }

    private async Task CalculateBeerTotalPrice(List<OrderDetail> orderDetails)
    {
        foreach (var orderDetail in orderDetails)
        {
            var beer = await beerRepository.GetBeerById(orderDetail.BeerId);

            orderDetail.Discount = orderDetail.Amount switch
                                   {
                                       >= 20 => 0.2f,
                                       >= 10 => 0.1f,
                                       _ => 0
                                   };
            orderDetail.Price = beer.WholesalePrice;
            orderDetail.TotalPrice = orderDetail.Amount * beer.WholesalePrice * (1 - (decimal)orderDetail.Discount);
        }
    }
}
