using Dapper;
using ExerciseTracker.Data;
using ExerciseTracker.Models;

namespace ExerciseTracker.Repository;

public class RunningRepository(ExerciseDapperContext context)
{
    public async Task AddAsync(Running exercise)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        try
        {
            using var connection = context.GetConnection();
            var result = await connection.ExecuteAsync(
                "INSERT INTO Running (Comments, DateEnd, DateStart, Duration) VALUES (@Comments, @DateEnd, @DateStart, @Ticks)",
                exercise);
            if (result != 1) throw new Exception("Something wrong with inserting");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Running>> GetAllAsync()
    {
        try
        {
            using var connection = context.GetConnection();
            var result =
                await connection.QueryAsync<Running>(
                    "SELECT Id, Comments, DateEnd, DateStart, Duration as Ticks FROM Running;");
            return result.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteAsync(Running exercise)
    {
        ArgumentNullException.ThrowIfNull(exercise);

        try
        {
            using var connection = context.GetConnection();
            var result = await connection.ExecuteAsync("DELETE FROM Running WHERE Id = @Id", exercise);
            if (result != 1) throw new Exception("Something went wrong with deleting");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateAsync(Running running)
    {
        ArgumentNullException.ThrowIfNull(running);

        try
        {
            using var connection = context.GetConnection();
            var result = await connection.ExecuteAsync(
                "UPDATE Running SET Comments = @Comments, DateEnd = @DateEnd, DateStart = @DateStart, Duration = @Ticks WHERE Id = @Id",
                running);
            if (result != 1) throw new Exception("Something went wrong with updating");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}