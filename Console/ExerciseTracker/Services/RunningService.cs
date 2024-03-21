using ExerciseTracker.Models;
using ExerciseTracker.Repository;

namespace ExerciseTracker.Services;

public class RunningService(RunningRepository repository)
{
    public async Task<List<Running>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task AddAsync(Running model)
    {
        if (model.DateStart > model.DateEnd)
            throw new ArgumentException("DateEnd must be greater than DateStart");

        model.Duration = model.DateEnd - model.DateStart;

        await repository.AddAsync(model);
    }

    public async Task UpdateAsync(Running model)
    {
        if (model.DateStart > model.DateEnd)
            throw new ArgumentException("DateEnd must be greater than DateStart");

        model.Duration = model.DateEnd - model.DateStart;

        await repository.UpdateAsync(model);
    }

    public async Task DeleteAsync(Running model)
    {
        if ((await repository.GetAllAsync()).TrueForAll(m => m.Id != model.Id))
            throw new ArgumentException("No running exercise found with that ID");

        await repository.DeleteAsync(model);
    }
}