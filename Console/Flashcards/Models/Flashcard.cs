namespace Flashcards.Models;

public class Flashcard
{
    public int Id { get; set; }
    [ForeignKey("Stack")]
    public int StackId { get; set; }
    public Stack Stack { get; set; }
    [Column(TypeName = "nvarchar(50)")]
    public string Title { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string Question { get; set; }
    [Column(TypeName = "nvarchar(250)")]
    public string Answer { get; set; }
}