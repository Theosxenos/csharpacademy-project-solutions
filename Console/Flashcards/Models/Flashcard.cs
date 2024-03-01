namespace Flashcards.Models;

public class Flashcard
{
    public int Id { get; set; }
    [ForeignKey("Stack")]
    public int StackId { get; set; }
    public Stack Stack { get; set; }
    public string Title { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}