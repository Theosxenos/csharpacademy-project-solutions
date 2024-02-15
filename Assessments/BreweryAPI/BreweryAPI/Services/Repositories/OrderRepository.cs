namespace BreweryAPI.Services.Repositories;

public class OrderRepository(BreweryDbContext breweryDbContext) : Repository<Order>(breweryDbContext), IOrderRepository
{
    public new IQueryable<Order> GetAll()
    {
        return breweryDbContext.Orders
            .Include(o => o.Brewery)
            .Include(o => o.OrderDetails).ThenInclude(od => od.Beer)
            .Include(o => o.Wholesaler);
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await breweryDbContext.Orders.FindAsync(orderId);
    }
}
