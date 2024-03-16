using Microsoft.EntityFrameworkCore;
using TodoAppI.Models;

namespace TodoAppI.Data;

public class TodoContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().HasData([
            new TodoItem
            {
                Id = 1,
                Name = "Learn C# basics",
                Completed = true
            },
            new TodoItem
            {
                Id = 2,
                Name = "Finish C# Academy",
                Completed = false
            },
            new TodoItem
            {
                Id = 3,
                Name = "Get job",
                Completed = false
            },
            new TodoItem
            {
                Id = 4,
                Name = "????",
                Completed = true
            },
            new TodoItem
            {
                Id = 5,
                Name = "Profit",
                Completed = false
            }
        ]);
    }
}