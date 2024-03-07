using ExerciseTracker.Repository;
using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class SquatsController(SquatsRepository repository)
{
    private SquatsView view = new();

    public async Task ShowSquatsMenu()
    {
        var menu = new Dictionary<string, Func<Task>>
        {
            ["Add"] = () => Task.CompletedTask,
            ["Manage"] = ManageSquat,
            ["Exit"] = () => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke();
    }

    public async Task ManageSquat()
    {
        var squats = await repository.GetAllSquatsAsync();
        if (squats.Count == 0)
        {
            view.ShowError("No squat logs found.");
        }

        var chosenSquat = view.ShowSquatLogsMenu(squats);
    }
}