namespace BreweryAPI.Services;

public interface IBeerRepository :  IRepository<Beer>
{
    public Task<List<Beer>> GetAllBeers();
    Task<List<Beer>> GetBeersByBreweryId(int breweryId);
    Task<Beer> DeleteBeerById(int beerId);
    Task<Beer> UpdateBeer(BeerRequest updatedBeer, int beerId);
    Task<Beer?> GetBeerById(int beerId);
}
