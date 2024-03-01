namespace CodingTracker.Views;

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
}