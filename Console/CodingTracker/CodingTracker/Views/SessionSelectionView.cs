namespace CodingTracker.Views;

public class SessionSelectionView: BaseView
{
    public Session Prompt(List<Session> sessions)
    {
        var orderedSessions = sessions.OrderBy(s => s.Day);
        
        return AnsiConsole.Prompt(
            new SelectionPrompt<Session>()
                .Title("Select a [green]session[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more sessions)[/]")
                .AddChoices(orderedSessions)
        );
    }
}