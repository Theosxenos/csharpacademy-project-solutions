namespace BreweryAPI.Services.Repositories;

public class BreweryRepository(BreweryDbContext breweryDbContext) : Repository<Brewery>(breweryDbContext),IBreweryRepository
{
    public async Task<bool> DoesBreweryExist(int breweryId)
    {
        return await GetAll().FirstOrDefaultAsync(b => b.Id == breweryId) != null;
    }
}