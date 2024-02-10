using CodingTracker.Models;

namespace CodingTracker.Views;

public class CreateSessionLogView
{
    public SessionLog Prompt()
    {
        // AnsiConsole.Clear(); // Uncomment if you want to clear the console.

        AnsiConsole.MarkupLine("Start a New Coding Session");

        // Get start time with shared validation logic
        var startTime = AskForTime("At what time did you start? [grey](HH:mm)[/]");

        // Get stop time with the same validation logic
        var endTime = AskForTime("At what time did you stop? [grey](HH:mm)[/]");

        return new()
        {
            StartTime = startTime,
            EndTime = endTime
        };
    }

    private TimeOnly AskForTime(string promptMessage)
    {
        var timeAsString = AnsiConsole.Prompt(
            new TextPrompt<string>(promptMessage)
                .ValidationErrorMessage("[red]Please enter a valid time in 24-hour format (HH:mm).[/]")
                .Validate(input =>
                {
                    // Attempt to parse the input string as TimeOnly in 24-hour format
                    if (TimeOnly.TryParseExact(input, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                    {
                        return ValidationResult.Success();
                    }

                    return ValidationResult.Error("[red]Invalid time format. Use 24-hour format (HH:mm).[/]");
                }));

        // Since the input has been validated, directly parse it to TimeOnly
        return TimeOnly.ParseExact(timeAsString, "HH:mm", CultureInfo.InvariantCulture);
    }

    public void ShowError(string message)
    {
        AnsiConsole.MarkupLine($"[red] {message}[/]");
    }
}
