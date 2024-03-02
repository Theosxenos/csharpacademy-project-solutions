namespace Flashcards.Models;

public class Stack
{
    public int Id { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; }
    public List<Flashcard> Flashcards { get; set; }
}