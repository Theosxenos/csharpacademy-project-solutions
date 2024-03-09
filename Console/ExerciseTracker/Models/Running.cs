namespace ExerciseTracker.Models;

public class Running : BaseModel
{
    public long Ticks
    {
        get => Duration.Ticks;
        set => Duration = new TimeSpan(value);
    }
}