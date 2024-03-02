namespace Flashcards.Views;

public class CreateStackView : BaseView
{
    public string Prompt()
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("What's the name of the stack:")
            .ValidationErrorMessage("[red]The name must be at least 1 character, and at most 50 characters[/]")
            .Validate(i => i.Trim().Length is <= 50 and >= 1));
    }
}