namespace CodingTracker.Views;

public class CreateSessionView: BaseView
{
    public Session Prompt(List<Session> sessions)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<Session>()
                .Title("Select a [green]session[/]")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more sessions)[/]")
                .AddChoices(sessions)
        );
    }
}