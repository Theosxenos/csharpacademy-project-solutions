namespace CodingTracker.Models;

public class SessionLog
{
    public int Id { get; set; }
    public int SessionId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Double DurationInHours => (EndTime - StartTime).TotalHours;
}