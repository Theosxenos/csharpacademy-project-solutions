using ExerciseTracker.Models;
using ExerciseTracker.Services;
using ExerciseTracker.Views;

namespace ExerciseTracker.Controllers;

public class RunningController(RunningService service)
{
    private readonly ExerciseView view = new();

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
        try
        {
            var exercise = new Running();
            view.PromptUpsertExercise(exercise);

            await service.AddAsync(exercise);
            view.ShowSuccess($"Exercise from {exercise.DateStart.ToShortDateString()} created.");
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }

    private async Task ManageRunning()
    {
        var exercises = await service.GetAllAsync();
        if (exercises.Count == 0) view.ShowError("No running logs found.");

        var chosenExercise = view.ShowLogsMenu(exercises);
        await ShowRunningManageMenu((Running)chosenExercise);
    }

    private async Task ShowRunningManageMenu(Running chosenExercise)
    {
        var menu = new Dictionary<string, Func<Running, Task>>
        {
            ["Update"] = UpdateExercise,
            ["Delete"] = DeleteExercise,
            ["Exit"] = _ => Task.CompletedTask
        };

        var choice = view.ShowMenu(menu, converter: pair => pair.Key);
        await choice.Value.Invoke(chosenExercise);
    }

    private async Task DeleteExercise(Running arg)
    {
        try
        {
            await service.DeleteAsync(arg);
            view.ShowSuccess($"Exercise from {arg.DateStart} deleted successfully.");
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }

    private async Task UpdateExercise(Running arg)
    {
        try
        {
            view.PromptUpsertExercise(arg);
            await service.UpdateAsync(arg);
            view.ShowSuccess($"Exercise from {arg.DateStart} updated successfully.");
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }
}