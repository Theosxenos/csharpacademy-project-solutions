using ExerciseTracker.Data;
using ExerciseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Repository;

public class SquatsRepository(ExerciseContext context)
{
    public async Task<Squat> AddSquatAsync(Squat squat)
    {
        ArgumentNullException.ThrowIfNull(squat, nameof(squat));

        try
        {
            await context.Squats.AddAsync(squat);
            await context.SaveChangesAsync();

            return squat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Squat>> GetAllSquatsAsync()
    {
        try
        {
            return await context.Squats.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Squat> UpdateSquatAsync(Squat squat)
    {
        ArgumentNullException.ThrowIfNull(squat, nameof(squat));
        
        try
        {
            context.Squats.Update(squat);
            await context.SaveChangesAsync();
            
            return squat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Squat> DeleteSquat(Squat squat)
    {
        try
        {
            context.Squats.Remove(squat);
            await context.SaveChangesAsync();

            return squat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}