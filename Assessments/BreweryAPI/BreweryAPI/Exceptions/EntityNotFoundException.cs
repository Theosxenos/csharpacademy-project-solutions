namespace BreweryAPI.Exceptions;

public class EntityIdNotFoundException<TEntity> : Exception
{

    public EntityIdNotFoundException(int entityId) : base($"{typeof(TEntity).Name} with ID: {entityId} not found.")
    {
    }
    
    // Constructor that accepts a specific message.
    public EntityIdNotFoundException(string message) : base(message)
    {
    }

    // Constructor that accepts a specific message and an inner exception.
    public EntityIdNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}