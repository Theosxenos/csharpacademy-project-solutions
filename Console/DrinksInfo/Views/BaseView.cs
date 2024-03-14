namespace DrinksInfo.Views;

public class BaseView
{
    public void ShowError(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
        Console.ReadKey();
    }

    public T ShowMenu<T>(IEnumerable<T> menuOptions, string title = "Select a menu option:", int pageSize = 10)
        where T : notnull
    {
        AnsiConsole.Clear();
        return AnsiConsole.Prompt(new SelectionPrompt<T>()
            .Title(title)
            .PageSize(pageSize)
            .AddChoices(menuOptions));
    }
}