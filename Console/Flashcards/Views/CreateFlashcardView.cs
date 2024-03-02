namespace Flashcards.Views;

public class CreateFlashcardView : BaseView
{
    public Flashcard Prompt()
    {
        var title = AskInput("Pick a title:");
        var question = AskInput("Pick a question:");
        var answer = AskInput("Pick an answer:");

        return new()
        {
            Title = title.Trim(),
            Question = question.Trim(),
            Answer = answer.Trim()
        };
    }
}