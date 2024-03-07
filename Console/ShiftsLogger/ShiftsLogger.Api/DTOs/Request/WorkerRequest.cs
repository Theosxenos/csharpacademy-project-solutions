using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.Api.DTOs.Request;

public class WorkerRequest
{
    [Required]
    public string Name { get; set; }
}