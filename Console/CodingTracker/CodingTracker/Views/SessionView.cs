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
                    if (DateOnly.TryParseExact(input, dateFormat, out _)) return ValidationResult.Success();

                    return ValidationResult.Error($"[red]Invalid date format. Use a correct format ({dateFormat}).[/]");
                }));

        return DateOnly.ParseExact(startDate, dateFormat);
    }

    public void ShowSessionTree(List<Session> sessions, List<SessionLog> sessionLogs)
    {
        foreach (var session in sessions)
        {
            var table = new Table();
            table.Border = TableBorder.Rounded;
            table.AddColumn("Start Time")
                .AddColumn("End Time")
                .AddColumn("Duration (Hours)")
                .Title($"{session.Day.ToString(Validator.DateFormat)}");
            
            foreach (var log in sessionLogs.Where(l => l.SessionId == session.Id))
            {
                table.AddRow([$"{log.StartTime:HH:mm}", $"{log.EndTime:HH:mm}", $"{log.Duration.TotalHours:N1}"]);
            }
            
            AnsiConsole.Write(table);

        }

        AnsiConsole.MarkupLine("[grey]Press any key to go back to the menu.[/]");
        Console.ReadKey();
    }
}