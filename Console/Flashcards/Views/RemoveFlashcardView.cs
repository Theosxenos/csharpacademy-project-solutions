namespace Flashcards.Views;

public class RemoveFlashcardView : BaseView
{
    public List<Flashcard> Prompt(List<Flashcard> chosenStackFlashcards)
    {
        return AnsiConsole.Prompt(new MultiSelectionPrompt<Flashcard>()
            .Title("Select the flashcards to delete")
            .AddChoices(chosenStackFlashcards)
        );
    }
}