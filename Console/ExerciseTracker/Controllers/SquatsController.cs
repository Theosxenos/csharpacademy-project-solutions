using ExerciseTracker.Models;
using ExerciseTracker.Repository;
using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class SquatsController(SquatsRepository repository)
{
    private ExerciseView view = new();

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
        var squat = new Squat();
        view.PromptUpsertSquat(squat);

        await repository.AddSquatAsync(squat);
        view.ShowSuccess($"Squat from {squat.DateStart.ToShortDateString()} created.");
    }

    public async Task ManageSquat()
    {
        var squats = await repository.GetAllSquatsAsync();
        if (squats.Count == 0)
        {
            view.ShowError("No squat logs found.");
        }

        var chosenSquat = view.ShowLogsMenu(squats);
        await ShowSquatManageMenu((Squat)chosenSquat);
    }

    public async Task ShowSquatManageMenu(Squat squat)
    {
        var menu = new Dictionary<string, Func<Squat, Task>>
        {
            ["Update"] =  UpdateSquat,
            ["Delete"] = DeleteSquat,
            ["Exit"] = _ => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke(squat);
    }

    private async Task DeleteSquat(Squat arg)
    {
        _ = await repository.DeleteSquat(arg);
        view.ShowSuccess($"Squat from {arg.DateStart.ToShortDateString()} deleted.");
    }

    private async Task UpdateSquat(Squat arg)
    {
        view.PromptUpsertSquat(arg);
        var updated = await repository.UpdateSquatAsync(arg);
        view.ShowSuccess($"Squat from {arg.DateStart.ToShortDateString()} update.");
    }
}