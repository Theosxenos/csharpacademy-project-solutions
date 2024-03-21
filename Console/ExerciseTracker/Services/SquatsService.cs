using ExerciseTracker.Models;
using ExerciseTracker.Repository;

namespace ExerciseTracker.Services;

public class SquatsService(SquatsRepository repository)
{
    public async Task<List<Squat>> GetAllAsync()
    {
        return await repository.GetAllSquatsAsync();
    }

    public async Task AddAsync(Squat model)
    {
        if (model.DateStart > model.DateEnd)
            throw new ArgumentException("DateEnd must be greater than DateStart");

        model.Duration = model.DateEnd - model.DateStart;

        await repository.AddSquatAsync(model);
    }

    public async Task UpdateAsync(Squat model)
    {
        if (model.DateStart > model.DateEnd)
            throw new ArgumentException("DateEnd must be greater than DateStart");

        model.Duration = model.DateEnd - model.DateStart;

        await repository.UpdateSquatAsync(model);
    }

    public async Task DeleteAsync(Squat model)
    {
        if ((await repository.GetAllSquatsAsync()).TrueForAll(m => m.Id != model.Id))
            throw new ArgumentException("No Squat exercise found with that ID");

        await repository.DeleteSquatAsync(model);
    }
}