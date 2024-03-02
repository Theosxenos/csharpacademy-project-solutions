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

    public T ShowMenu<T>(IEnumerable<T> menuOptions, string title) where T : notnull
    {
        return AnsiConsole.Prompt(new SelectionPrompt<T>()
            .Title(title)
            .AddChoices(menuOptions));
    }
}