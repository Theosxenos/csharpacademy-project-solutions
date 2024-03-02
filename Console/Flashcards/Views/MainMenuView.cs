namespace Flashcards.Views;

public class MenuView
{
    public string ShowMenu(string[] menuOptions)
    {
        AnsiConsole.Clear();
        return AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Select a menu option:")
            .PageSize(10)
            .AddChoices(menuOptions));
    }
}