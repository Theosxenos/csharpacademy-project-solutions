namespace Flashcards.Models;

public class Session
{
    public int Id { get; set; }
    [ForeignKey("Stack")]
    public int StackId { get; set; }
    public Stack Stack { get; set; }
    public int Score { get; set; }
    public DateTime SessionDate { get; set; }
}