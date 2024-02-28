namespace BreweryAPI.Services;

public class PlaceOrderService(IOrderRepository orderRepository, IBeerRepository beerRepository, IWholesalerInventoryService wholesalerInventoryService, IMapper mapper) : IPlaceOrderService
{
    public async Task<OrderResponse> PlaceWholesalerOrder(WholesalerBuyOrderRequest buyOrder)
    {
        var order = mapper.Map<Order>(buyOrder);

        await CalculateBeerTotalPrice(order.OrderDetails);
        CalculateOrderTotalPrice(order);

        var result = await orderRepository.AddAsync(order);

        foreach (var orderedBeer in buyOrder.OrderedBeers)
        {
            await wholesalerInventoryService.UpsertWholesalerInventoryItem(buyOrder.WholesalerId.Value, orderedBeer.BeerId.Value,
                orderedBeer.Amount.Value);
        }

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
            var beer = await beerRepository.GetBeerById(orderDetail.BeerId!.Value);

            orderDetail.Price = beer.WholesalePrice;
            orderDetail.TotalPrice = orderDetail.Amount * beer.WholesalePrice;
        }
    }
}
