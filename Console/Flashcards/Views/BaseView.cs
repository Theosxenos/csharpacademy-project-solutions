namespace Flashcards.Views;

public class BaseView
{
    public void ShowSuccess(string message)
    {
        AnsiConsole.MarkupLine($"[green]{message}[/]");
        Console.ReadKey();
    }

    public void ShowError(string message)
    {
        AnsiConsole.MarkupLine($"[red]{message}[/]");
        Console.ReadKey();
    }

    public bool AskConfirm(string message)
    {
        return AnsiConsole.Confirm(message);
    }

    public string AskInput(string prompt)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>(prompt));
    }

    public T AskInput<T>(string prompt, Func<T, bool> validator, string errorMessage)
    {
        return AnsiConsole.Prompt(new TextPrompt<T>(prompt)
            .Validate(validator, errorMessage)
        );
    }
}