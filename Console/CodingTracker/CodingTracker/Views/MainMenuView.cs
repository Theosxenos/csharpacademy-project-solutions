namespace CodingTracker.Views;

public class MainMenuView
{
    public string ShowMenu(string[] menuItems)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[bold yellow]Spectre.Console Demo Application[/]");

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(menuItems));
    }
}
