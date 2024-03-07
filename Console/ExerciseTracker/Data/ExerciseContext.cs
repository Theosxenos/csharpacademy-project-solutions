using ExerciseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Data;

public class ExerciseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Squat> Squats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Squat>().HasData([
            new()
            {
                Id = 1,
                Date = DateTime.Today.AddDays(-2),
                Weight = 10f
            },
            new ()
            {
                Id = 2,
                Date = DateTime.Today.AddDays(-1),
                Weight = 12.5f
            },
            new ()
            {
                Id = 3,
                Date = DateTime.Today,
                Weight = 15f
            }
        ]);
    }
}