using Spectre.Console;

namespace ExerciseTracker.Views;

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

    public void ShowMessage(string message)
    {
        AnsiConsole.MarkupLine(message);
        Console.ReadKey();
    }

    public bool AskConfirm(string message)
    {
        return AnsiConsole.Confirm(message);
    }

    public string AskInput(string prompt, string? defaultValue = null)
    {
        var textPrompt = new TextPrompt<string>(prompt);
        if (!string.IsNullOrEmpty(defaultValue)) textPrompt.DefaultValue(defaultValue);

        return AnsiConsole.Prompt(textPrompt);
    }

    public T AskInput<T>(string prompt, Func<T, bool> validator, string errorMessage)
    {
        return AnsiConsole.Prompt(new TextPrompt<T>(prompt)
            .Validate(validator, errorMessage)
        );
    }
    
    public T ShowMenu<T>(IEnumerable<T> menuOptions, string title = "Select a menu option:", int pageSize = 10, Func<T, string>? converter = null)
        where T : notnull
    {
        AnsiConsole.Clear();

        string DefaultConverter(T item) => item.ToString() ?? throw new InvalidOperationException($"Cannot convert {typeof(T)} to string.");
        var prompt = new SelectionPrompt<T>()
        {
            Title = title,
            PageSize = pageSize,
            Converter = converter ?? DefaultConverter,
        };
        prompt.AddChoices(menuOptions);

        return AnsiConsole.Prompt(prompt);
    }
}