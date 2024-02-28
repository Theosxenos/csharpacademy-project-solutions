namespace BreweryAPI.Services.Repositories;

public class BeerRepository (IDbContextFactory<BreweryDbContext> breweryDbFactory, BreweryDbContext dbContext, IBreweryRepository breweryRepository, IMapper mapper) : Repository<Beer>(dbContext), IBeerRepository
{
    public async Task<List<Beer>> GetAllBeers()
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();
        return await breweryDbContext.Beers.Include(b => b.Brewery).ToListAsync();
    }

    public async Task<Beer> GetBeerById(int beerId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        var beer = await breweryDbContext.Beers.FirstOrDefaultAsync(b => b.Id == beerId);

        return beer ?? throw new EntityIdNotFoundException<Beer>(beerId);
    }

    public async Task<List<Beer>> GetBeersByBreweryId(int breweryId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        var isBreweryValid = await breweryRepository.DoesBreweryExist(breweryId);
        if (!isBreweryValid)
        {
            throw new EntityIdNotFoundException<Brewery>(breweryId);
        }
        
        return await breweryDbContext.Beers.Where(b => b.BreweryId == breweryId).ToListAsync();
    }

    public async Task<Beer> DeleteBeerById(int beerId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        var toDeleteBeer = await GetBeerById(beerId);
        
        breweryDbContext.Remove(toDeleteBeer);
        await breweryDbContext.SaveChangesAsync();
        return toDeleteBeer;
    }

    public async Task<Beer> UpdateBeer(BeerRequest updatedBeer, int beerId)
    {
        await using var breweryDbContext = await breweryDbFactory.CreateDbContextAsync();

        var beerToUpdate = await GetBeerById(beerId);

        mapper.Map(updatedBeer, beerToUpdate);
        breweryDbContext.Beers.Update(beerToUpdate);
        await breweryDbContext.SaveChangesAsync();

        return beerToUpdate;
    }

}
