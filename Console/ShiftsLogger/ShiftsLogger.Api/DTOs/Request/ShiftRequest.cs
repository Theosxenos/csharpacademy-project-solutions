using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Api.DTOs.Request;

public class ShiftRequest
{
    public int WorkerId { get; set; }
    [Required]
    public DateTime StartShift { get; set; }
    [Required]
    public DateTime EndShift { get; set; }
}