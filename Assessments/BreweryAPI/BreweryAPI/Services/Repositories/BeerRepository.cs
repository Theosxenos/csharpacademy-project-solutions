namespace BreweryAPI.Services.Repositories;

public class BeerRepository (IDbContextFactory<BreweryDbContext> breweryDbFactory, BreweryDbContext dbContext, IMapper mapper) : Repository<Beer>(dbContext), IBeerRepository
{
    public async Task<List<Beer>> GetAllBeers()
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();
        return await breweryDbContext.Beers.Include(b => b.Brewery).ToListAsync();
    }

    public async Task<Beer?> GetBeerById(int beerId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        return await breweryDbContext.Beers.FirstOrDefaultAsync(b => b.Id == beerId);
    }

    public async Task<List<Beer>> GetBeersByBreweryId(int breweryId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        return await breweryDbContext.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
    }

    public async Task<Beer> DeleteBeerById(int beerId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        var toDeleteBeer = await GetBeerById(beerId);

        if (toDeleteBeer == null) return null; //TODO Return null or handle the case where beer is not found

        breweryDbContext.Remove(toDeleteBeer);
        await breweryDbContext.SaveChangesAsync();
        return toDeleteBeer;
    }

    public async Task<Beer> UpdateBeer(BeerRequest updatedBeer, int beerId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        var beerToUpdate = await GetBeerById(beerId);
        ArgumentNullException.ThrowIfNull(beerToUpdate);

        mapper.Map(updatedBeer, beerToUpdate);
        breweryDbContext.Beers.Update(beerToUpdate);
        await breweryDbContext.SaveChangesAsync();

        return beerToUpdate;
    }

}
