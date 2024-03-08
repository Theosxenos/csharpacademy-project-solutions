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
            var result = await connection.ExecuteAsync("INSERT INTO Running (Comments, DateEnd, DateStart, Duration) VALUES (@Comments, @DateEnd, @DateStart, @Duration)", exercise);
            if (result != 1)
            {
                throw new Exception("Something wrong with inserting");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}