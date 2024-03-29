using ExerciseTracker.Models;
using ExerciseTracker.Services;
using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class SquatsController(SquatsService service)
{
    private readonly ExerciseView view = new();

    public async Task ShowSquatsMenu()
    {
        var menu = new Dictionary<string, Func<Task>>
        {
            ["Add"] = AddSquat,
            ["Manage"] = ManageSquat,
            ["Exit"] = () => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke();
    }

    private async Task AddSquat()
    {
        try
        {
            var squat = new Squat();
            view.PromptUpsertExercise(squat);

            await service.AddAsync(squat);
            view.ShowSuccess($"Squat from {squat.DateStart.ToShortDateString()} created.");
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }

    public async Task ManageSquat()
    {
        var squats = await service.GetAllAsync();
        if (squats.Count == 0)
        {
            view.ShowError("No squat logs found.");
            return;
        }

        var chosenSquat = view.ShowLogsMenu(squats);
        await ShowSquatManageMenu((Squat)chosenSquat);
    }

    public async Task ShowSquatManageMenu(Squat squat)
    {
        var menu = new Dictionary<string, Func<Squat, Task>>
        {
            ["Update"] = UpdateSquat,
            ["Delete"] = DeleteSquat,
            ["Exit"] = _ => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke(squat);
    }

    private async Task DeleteSquat(Squat arg)
    {
        try
        {
            await service.DeleteAsync(arg);
            view.ShowSuccess($"Squat from {arg.DateStart.ToShortDateString()} deleted.");
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }

    private async Task UpdateSquat(Squat arg)
    {
        try
        {
            view.PromptUpsertExercise(arg);
            await service.UpdateAsync(arg);
            view.ShowSuccess($"Squat from {arg.DateStart.ToShortDateString()} update.");
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }
}