using System.Net.Http.Json;
using ShiftsLogger.Client.Views;
using ShiftsLogger.Shared.DTOs.Request;
using ShiftsLogger.Shared.Models;

namespace ShiftsLogger.Client.Controllers;

public class MainController
{
    private readonly HttpClient httpClient = new();
    private readonly MainView view = new();

    public async Task ShowMenu()
    {
        var runApp = true;
        var menu = new Dictionary<string, Func<Task>>
        {
            ["Add Worker"] = AddWorker,
            ["Log Shift"] = LogShift,
            ["List Shifts"] = ListShifts,
            ["Exit"] = () => Task.FromResult(runApp = false)
        };

        while (runApp)
            try
            {
                var choice = view.ShowMenu(menu.Keys.ToArray());
                await menu[choice]();
            }
            catch (HttpRequestException e)
            {
                view.ShowError($"Cannot connect to the API server. ERROR: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
    }

    private async Task ListShifts()
    {
        var workers = await httpClient.GetFromJsonAsync<List<Worker>>($"{Program.BaseUrl}/workers");
        if (workers == null)
        {
            view.ShowError("A problem occured after getting the list of workers. The returned worker object is null.");
            return;
        }

        if (workers.Count == 0)
        {
            view.ShowError("No workers found. Please add one before logging a shift.");
            return;
        }

        var selectedWorker = view.ShowMenu(workers.Select(w => w.Name));
        var workerId = workers.First(w => w.Name.Equals(selectedWorker)).Id;

        var shifts = await httpClient.GetFromJsonAsync<List<Shift>>($"{Program.BaseUrl}/shifts/{workerId}");
        if (shifts == null)
        {
            view.ShowError("A problem occured after getting the list of workers. The returned worker object is null.");
            return;
        }

        if (shifts.Count == 0)
        {
            view.ShowError($"No shifts found for worker {selectedWorker}");
            return;
        }

        view.ListShifts(shifts);
    }

    public async Task AddWorker()
    {
        var name = view.AskInput("What's the worker's name?");
        var responseMessage =
            await httpClient.PostAsJsonAsync($"{Program.BaseUrl}/workers", new WorkerRequest { Name = name });
        if (!responseMessage.IsSuccessStatusCode)
        {
            view.ShowError(responseMessage.ReasonPhrase ??
                           $"An error with the status code {responseMessage.StatusCode} occured");
            return;
        }

        var worker = await responseMessage.Content.ReadFromJsonAsync<Worker>();
        if (worker == null)
        {
            view.ShowError($"A problem occured after adding worker {name}. The returned worker object is null.");
            return;
        }

        view.ShowSuccess($"Worker {worker.Name} with ID {worker.Id} has been created.");
    }

    public async Task LogShift()
    {
        var workers = await httpClient.GetFromJsonAsync<List<Worker>>($"{Program.BaseUrl}/workers");
        if (workers == null)
        {
            view.ShowError("A problem occured after getting the list of workers. The returned worker object is null.");
            return;
        }

        if (workers.Count == 0)
        {
            view.ShowError("No workers found. Please add one before logging a shift.");
            return;
        }

        var selectedWorker = view.ShowMenu(workers.Select(w => w.Name));
        var workerId = workers.First(w => w.Name.Equals(selectedWorker)).Id;

        var retry = false;
        do
        {
            var shiftStart = view.AskStartShift();
            var shiftEnd = view.AskEndShift();

            if (shiftEnd < shiftStart)
            {
                view.ShowError("End shift can't be before the start shift.");
                retry = view.AskConfirm("Try again?");
            }
            else
            {
                var shift = new ShiftRequest
                {
                    WorkerId = workerId,
                    StartShift = shiftStart,
                    EndShift = shiftEnd
                };
                var response = await httpClient.PostAsJsonAsync($"{Program.BaseUrl}/shifts", shift);
                if (response.IsSuccessStatusCode)
                    view.ShowSuccess("Shift created successfully.");
                else
                    view.ShowError(response.ReasonPhrase ??
                                   $"An error with the status code {response.StatusCode} occured");

                retry = false;
            }
        } while (retry);
    }
}