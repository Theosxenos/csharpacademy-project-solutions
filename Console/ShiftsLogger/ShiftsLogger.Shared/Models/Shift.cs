namespace ShiftsLogger.Shared.Models;

public class Shift
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public Worker Worker { get; set; }
    public DateTime StartShift { get; set; }
    public DateTime EndShift { get; set; }
    public int MinutesDuration { get; set; }
}