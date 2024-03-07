using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Api.Data;
using ShiftsLogger.Api.Models;

namespace ShiftsLogger.Api.Repositories;

public class Repository(ShiftsLoggerContext context)
{
    public async Task<Worker> AddWorker(Worker worker)
    {
        context.Workers.Add(worker);
        await context.SaveChangesAsync();

        return worker;
    }

    public Task WorkerExistsByName(string name)
    {
        return context.Workers.AnyAsync(w => w.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}