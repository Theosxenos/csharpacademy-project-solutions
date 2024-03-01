namespace CodingTracker.Views;

public class SessionManagingView : BaseView
{
    public string ShowManagingMenu(string sessionDate, string[] menuOptions)
    {
        // AnsiConsole.MarkupLine($"What would you like to do with the session: [green]{sessionDate}[/]?");
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title($"What would you like to do with the session: [green]{sessionDate}[/]?")
            .AddChoices(menuOptions));
    }
}