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

    public void ShowSessionTree(List<Session> sessions, List<SessionLog> sessionLogs)
    {
        AnsiConsole.MarkupLine("Your sessions.");
        
        var rootNodes = new List<Tree>();
        var orderedSessionLogs = sessionLogs.OrderBy(l => l.StartTime).ToArray();
        
        foreach (var session in sessions.OrderBy(s => s.Day))
        {
            var root = new Tree(session.ToString());
            rootNodes.Add(root);
            
            foreach (var sessionLog in orderedSessionLogs.Where(l => l.SessionId == session.Id))
            {
                root.AddNode($"{sessionLog.StartTime} - {sessionLog.EndTime}");
            }
        }

        rootNodes.ForEach(AnsiConsole.Write);
        
        AnsiConsole.MarkupLine("[grey]Press any key to go back to the menu.[/]");
        Console.ReadKey();
    }
}
