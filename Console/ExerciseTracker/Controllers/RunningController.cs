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
        view.PromptUpsertExercise(exercise);

        await repository.AddAsync(exercise);
        view.ShowSuccess($"Exercise from {exercise.DateStart.ToShortDateString()} created.");
    }

    private async Task ManageRunning()
    {
        var exercises = await repository.GetAllAsync();
        if (exercises.Count == 0)
        {
            view.ShowError("No running logs found.");
        }

        var chosenExercise = view.ShowLogsMenu(exercises);
        await ShowRunningManageMenu((Running)chosenExercise);
    }

    private async Task ShowRunningManageMenu(Running chosenExercise)
    {
        var menu = new Dictionary<string, Func<Running, Task>>
        {
            ["Update"] =  UpdateExercise,
            ["Delete"] = DeleteExercise,
            ["Exit"] = _ => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke(chosenExercise);
    }

    private async Task DeleteExercise(Running arg)
    {
        await repository.DeleteAsync(arg);
        view.ShowSuccess($"Exercise from {arg.DateStart} deleted successfully.");

    }

    private async Task UpdateExercise(Running arg)
    {
        view.PromptUpsertExercise(arg);
        await repository.UpdateAsync(arg);
        view.ShowSuccess($"Exercise from {arg.DateStart} updated successfully.");
    }
}