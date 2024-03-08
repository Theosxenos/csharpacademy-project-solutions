using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class MainController(SquatsController squatsController)
{
    public async Task ShowMenu()
    {
        var menu = new Dictionary<string, Func<Task>>
        {
            ["Squats Menu"] = squatsController.ShowSquatsMenu,
            ["Running Menu"] = () => throw new NotImplementedException(),
            ["Exit"] = async () => Environment.Exit(0),
        };

        var choice = new BaseView().ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke();
    }
}