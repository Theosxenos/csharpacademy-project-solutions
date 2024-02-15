namespace BreweryAPI.Services.Repositories;

public class Repository<TEntity> (BreweryDbContext breweryDbContext) : IRepository<TEntity> where TEntity : class, new()
{

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return breweryDbContext.Set<TEntity>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't retrieve entities: {ex.Message}");
        }
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        //TODO Test
        ArgumentNullException.ThrowIfNull(entity);

        try
        {
            await breweryDbContext.AddAsync(entity);
            await breweryDbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }

        try
        {
            breweryDbContext.Update(entity);
            await breweryDbContext.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
        }
    }
}
