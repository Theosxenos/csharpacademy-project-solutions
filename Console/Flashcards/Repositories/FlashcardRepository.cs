namespace Flashcards.Repositories;

public class FlashcardRepository
{  
    private readonly FlaschardDatabase db = new();

    public void CreateFlashcard(Flashcard flashcard)
    {
        using var connection = db.GetConnection();
        connection.Execute("INSERT INTO Flashcards (StackId, Title, Question, Answer) VALUES (@StackId, @Title, @Question, @Answer);", flashcard);
    }

    public void UpdateFlashcard(Flashcard flashcard)
    {
        using var connection = db.GetConnection();
        connection.Execute("UPDATE Flashcards SET StackId = @StackId, Title = @Title, Question = @Question, Answer = @Answer WHERE Id = @Id", flashcard);
    }

    public void DeleteFlashcard(Flashcard flashcard)
    {
        using var connection = db.GetConnection();
        connection.Execute("DELETE FROM Flashcards WHERE Id = @Id", new { flashcard.Id });
    }
    
    public void DeleteFlashcards(List<Flashcard> flashcards)
    {
        using var connection = db.GetConnection();
        connection.Execute("DELETE FROM Flashcards WHERE Id = @Id", flashcards);
    }

    public void DeleteFlashcardsByStackId(int stackId)
    {
        using var connection = db.GetConnection();
        connection.Execute("DELETE FROM Flashcards WHERE StackId = @StackId", new { StackId = stackId });
    }
}