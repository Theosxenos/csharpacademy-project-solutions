namespace BreweryAPI.Interfaces.Repositories;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
}
