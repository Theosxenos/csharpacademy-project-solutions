using System.Globalization;
using ExerciseTracker.Models;
using Spectre.Console;

namespace ExerciseTracker.Views;

public class ExerciseView : BaseView
{
    public BaseModel ShowLogsMenu(IEnumerable<BaseModel> exercises)
    {
        return ShowMenu(exercises.OrderBy(s => s.DateStart), "Choose an exercise to manage:",
            converter: exercise =>
                $"{exercise.DateStart:d-M-y h:mm}\t\t{exercise.Duration:d\\.h\\:mm}\t\t{exercise.Comments}");
    }

    public void PromptUpsertExercise(BaseModel exercise)
    {
        var promptStart = $"What's the start date/time of your {exercise.GetType().Name} session?";
        var promptEnd = $"What's the end date/time of your {exercise.GetType().Name} session?";
        var dateParsePattern =
            $"{CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern} {CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern}";
        var errorMessage = $"[red]Invalid date/time. The pattern is: {dateParsePattern}[/]";
        var validator = (string x) => DateTime.TryParse(x, out _);

        AnsiConsole.WriteLine($"The pattern is: {dateParsePattern}");

        bool retry;
        do
        {
            retry = false;

            var dateStartInput = AskInput(promptStart, validator, errorMessage,
                exercise.DateStart.ToString(CultureInfo.CurrentCulture));
            var dateEndInput = AskInput(promptEnd, validator, errorMessage,
                exercise.DateEnd.ToString(CultureInfo.CurrentCulture));

            var dateStart = DateTime.Parse(dateStartInput);
            var dateEnd = DateTime.Parse(dateEndInput);

            var comments = AskInput("Write a comment on your session:",
                defaultValue: string.IsNullOrEmpty(exercise.Comments) ? null : exercise.Comments);

            exercise.DateStart = dateStart;
            exercise.DateEnd = dateEnd;
            exercise.Comments = comments;
        } while (retry);
    }
}