using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Shared.Models;

namespace ShiftsLogger.Api.Data;

public class ShiftsLoggerContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Worker> Workers { get; set; } = default!;
    public DbSet<Shift> Shifts { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>().HasData(
        [
            new Worker
            {
                Id = 1,
                Name = "Henk"
            },
            new Worker
            {
                Id = 2,
                Name = "Ingrid"
            },
            new Worker
            {
                Id = 3,
                Name = "Jan"
            },
            new Worker
            {
                Id = 4,
                Name = "Helena"
            }
        ]);

        List<Shift> shifts = [];
        var lastId = 1;
        for (var i = 1; i < 5; i++)
        {
            var start = new DateTime(2024, 03, 15, 12, 0, 0);
            for (var day = 0; day < 3; day++)
                shifts.AddRange([
                    new Shift
                    {
                        Id = lastId++,
                        StartShift = start.AddDays(day),
                        EndShift = start.AddDays(day).AddHours(2),
                        Duration = TimeSpan.FromHours(2),
                        WorkerId = i
                    },
                    new Shift
                    {
                        Id = lastId++,
                        StartShift = start.AddDays(day).AddHours(3),
                        EndShift = start.AddDays(day).AddHours(5),
                        Duration = TimeSpan.FromHours(2),
                        WorkerId = i
                    },
                    new Shift
                    {
                        Id = lastId++,
                        StartShift = start.AddDays(day).AddHours(6),
                        EndShift = start.AddDays(day).AddHours(9),
                        Duration = TimeSpan.FromHours(3),
                        WorkerId = i
                    }
                ]);
        }

        modelBuilder.Entity<Shift>().HasData(shifts);
    }
}