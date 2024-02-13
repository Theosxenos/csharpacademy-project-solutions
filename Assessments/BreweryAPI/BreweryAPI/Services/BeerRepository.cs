namespace BreweryAPI.Services;

public class BeerRepository (BreweryDbContext breweryDbContext) : Repository<Beer>(breweryDbContext), IBeerRepository
{
    public async Task<List<Beer>> GetAllBeers()
    {
        return await breweryDbContext.Beers.Include(b => b.Brewery).ToListAsync();
    }

    public async Task<Beer?> GetBeerById(int beerId)
    {
        return await breweryDbContext.Beers.FirstOrDefaultAsync(b => b.Id == beerId);
    }

    public async Task<List<Beer>> GetBeersByBreweryId(int breweryId)
    {
        return await breweryDbContext.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
    }

    public async Task<Beer> DeleteBeerById(int beerId)
    {
        var toDeleteBeer = await GetBeerById(beerId);

        if (toDeleteBeer == null) return null; //TODO Return null or handle the case where beer is not found

        breweryDbContext.Remove(toDeleteBeer);
        await breweryDbContext.SaveChangesAsync();
        return toDeleteBeer;
    }

}