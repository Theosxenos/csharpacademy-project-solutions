using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class MainController(SquatsController squatsController, RunningController runningController)
{
    public async Task ShowMenu()
    {
        var menu = new Dictionary<string, Func<Task>>
        {
            ["Squats Menu"] = squatsController.ShowSquatsMenu,
            ["Running Menu"] = runningController.ShowRunningMenu,
            ["Exit"] = async () => Environment.Exit(0)
        };

        var choice = new BaseView().ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke();
    }
}