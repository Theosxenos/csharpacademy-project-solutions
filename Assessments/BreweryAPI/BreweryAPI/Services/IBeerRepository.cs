namespace BreweryAPI.Services;

public interface IBeerRepository :  IRepository<Beer>
{
    public Task<List<Beer>> GetAllBeers();
    Task<List<Beer>> GetBeersByBreweryId(int breweryId);
    Task<Beer> DeleteBeerById(int beerId);
}
