namespace ExerciseTracker.Models;

public class BaseModel
{
    public int Id { get; set; }
    public DateTime DateStart { get; set; } = DateTime.Now.AddHours(-1);
    public DateTime DateEnd { get; set; } = DateTime.Now;
    public TimeSpan Duration { get; set; }
    public string Comments { get; set; } = string.Empty;
}