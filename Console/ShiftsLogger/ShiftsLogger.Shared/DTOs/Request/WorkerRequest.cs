using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Shared.DTOs.Request;

public class WorkerRequest
{
    [Required] public string Name { get; set; }
}