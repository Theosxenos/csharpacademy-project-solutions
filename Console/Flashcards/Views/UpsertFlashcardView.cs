namespace Flashcards.Views;

public class UpsertFlashcardView : BaseView
{
    public void UpdateFlashcard(Flashcard flashcard)
    {
        flashcard.Title = AskInput("Pick a title:", flashcard.Title);
        flashcard.Question = AskInput("Pick a question:", flashcard.Question);
        flashcard.Answer = AskInput("Pick an answer:", flashcard.Answer);
    }

    public Flashcard CreateFlashcard()
    {
        var flashcard = new Flashcard();
        UpdateFlashcard(flashcard);
        return flashcard;
    }
}