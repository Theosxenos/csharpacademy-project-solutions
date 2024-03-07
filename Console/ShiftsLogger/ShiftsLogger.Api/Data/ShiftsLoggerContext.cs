using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Api.Models;

namespace ShiftsLogger.Api.Data;

public class ShiftsLoggerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Worker> Workers { get; set; }
    public DbSet<Shift> Shifts { get; set; }
}