namespace CodingTracker.Views;

public class SessionView : BaseView
{
    public DateOnly PromptStartSession()
    {
        var dateFormat = "d-M-yy";
        var startDate = AnsiConsole.Prompt(
            new TextPrompt<string>($"What's the day of the start of the session? [grey]({dateFormat})[/]")
                .ValidationErrorMessage($"[red]Invalid time format. Use a correct format ({dateFormat}).[/]")
                .Validate(input =>
                {
                    if (DateOnly.TryParseExact(input, dateFormat, out _))
                    {
                        return ValidationResult.Success();
                    }

                    return ValidationResult.Error($"[red]Invalid date format. Use a correct format ({dateFormat}).[/]");
                }));

        return DateOnly.ParseExact(startDate, dateFormat);
    }
}

public abstract class BaseView
{
    public void ShowSuccess(string message)
    {
        AnsiConsole.MarkupLine($"[green]{message}[/]");
    }
    public void ShowError(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
    }
}
