using ExerciseTracker.Models;
using ExerciseTracker.Repository;
using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class RunningController(RunningRepository repository)
{
    private ExerciseView view = new();
    public async Task ShowRunningMenu()
    {
        var menu = new Dictionary<string, Func<Task>>
        {
            ["Add"] = AddRunning,
            ["Manage"] = ManageRunning,
            ["Exit"] = () => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke();
    }

    private async Task AddRunning()
    {
        var exercise = new Running();
        view.PromptUpsertSquat(exercise);

        await repository.AddAsync(exercise);
        view.ShowSuccess($"Squat from {exercise.DateStart.ToShortDateString()} created.");
    }

    private Task ManageRunning()
    {
        throw new NotImplementedException();
    }
}