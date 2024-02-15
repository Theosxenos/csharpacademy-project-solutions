namespace BreweryAPI.Services.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    new IQueryable<Order> GetAll();
    Task<Order> GetOrderById(int orderId);
}
