using ExerciseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTracker.Data;

public class ExerciseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Squat> Squats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Squat>().HasData([
            new Squat
            {
                Id = 1,
                DateStart = DateTime.Now.AddDays(-2),
                DateEnd = DateTime.Now.AddDays(-2).AddHours(3),
                Duration = TimeSpan.FromHours(3),
                Comments = "Set PR at 50kg"
            },
            new Squat
            {
                Id = 2,
                DateStart = DateTime.Now.AddDays(-1),
                DateEnd = DateTime.Now.AddDays(-1).AddHours(0.25),
                Duration = TimeSpan.FromHours(0.25),
                Comments = "Quit after 3 reps"
            },
            new Squat
            {
                Id = 3,
                DateStart = DateTime.Now,
                DateEnd = DateTime.Now.AddHours(2),
                Duration = TimeSpan.FromHours(2),
                Comments = ""
            }
        ]);
    }
}