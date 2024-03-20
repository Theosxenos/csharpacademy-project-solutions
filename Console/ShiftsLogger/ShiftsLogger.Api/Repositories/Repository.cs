using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Api.Data;
using ShiftsLogger.Shared.DTOs.Request;
using ShiftsLogger.Shared.Models;

namespace ShiftsLogger.Api.Repositories;

public class Repository(ShiftsLoggerContext context)
{
    public async Task<Worker> AddWorker(Worker worker)
    {
        context.Workers.Add(worker);
        await context.SaveChangesAsync();

        return worker;
    }

    public Task<bool> WorkerExistsByName(string name)
    {
        return context.Workers.AnyAsync(w => w.Name.ToLower() == name.Trim().ToLower());
    }

    public Task<List<Worker>> GetAllWorkers()
    {
        return context.Workers.ToListAsync();
    }

    public async Task AddShift(ShiftRequest shift)
    {
        var toAddShift = new Shift
        {
            WorkerId = shift.WorkerId,
            StartShift = shift.StartShift,
            EndShift = shift.EndShift,
            Duration = shift.EndShift - shift.StartShift
        };
        context.Shifts.Add(toAddShift);
        await context.SaveChangesAsync();
    }

    public async Task<List<Shift>> GetShiftsByWorkerId(int workerId)
    {
        return await context.Shifts.Where(s => s.WorkerId == workerId).OrderBy(s => s.StartShift).ToListAsync();
    }
}