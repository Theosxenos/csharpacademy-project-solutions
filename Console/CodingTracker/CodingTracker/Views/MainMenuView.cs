namespace CodingTracker.Views;

public class MainMenuView
{
    public string ShowMenu(string[] menuItems)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Coding Tracker").Centered().Color(Color.Red));

        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(menuItems));
    }
}
