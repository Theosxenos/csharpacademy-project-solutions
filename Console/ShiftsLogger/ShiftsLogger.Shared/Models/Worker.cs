namespace ShiftsLogger.Shared.Models;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Shift> Shifts { get; set; }
}