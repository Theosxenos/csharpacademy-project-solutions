namespace BreweryAPI.Services.Repositories;

public interface IBreweryRepository : IRepository<Brewery>
{
    Task<bool> DoesBreweryExist(int breweryId);
}