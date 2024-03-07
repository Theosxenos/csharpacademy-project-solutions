using ShiftsLogger.Api.DTOs.Request;
using ShiftsLogger.Api.Models;
using ShiftsLogger.Api.Repositories;

namespace ShiftsLogger.Api.Services;

public class WorkerService(Repository repository)
{
    public async Task<Worker> AddWorker(WorkerRequest worker)
    {
        ArgumentNullException.ThrowIfNull(worker);

        if (string.IsNullOrEmpty(worker.Name?.Trim()))
        {
            throw new ArgumentException("Name cannot be empty");
        }

        return await repository.AddWorker(new Worker() { Name = worker.Name });
    }
}