namespace Flashcards.Data;

public class FlashcardsContext : DbContext
{
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<Stack> Stacks { get; set; }
    public DbSet<Session> Sessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        
        var connectionString = Program.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = Program.Configuration.GetConnectionString("SecretConnection");
        }

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Stack>()
        //     .HasMany<Flashcard>(s => s.Flashcards)
        //     .WithOne(f => f.Stack)
        //     .HasForeignKey(f => f.StackId);

        modelBuilder.Entity<Stack>()
            .HasIndex(s => s.Name)
            .IsUnique();
        
        SeedStack(modelBuilder);
        SeedFlashcards(modelBuilder);
        SeedSessions(modelBuilder);
    }

    private void SeedSessions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Session>().HasData([
            new ()
            {
                Id = 1,
                StackId = 1,
                Score = 20,
                SessionDate = DateTime.Parse("2024-03-01 12:00").ToUniversalTime()
            },
            new ()
            {
                Id = 2,
                StackId = 1,
                Score = 80,
                SessionDate = DateTime.Parse("2024-03-01 13:00").ToUniversalTime()
            },
            new ()
            {
                Id = 3,
                StackId = 2,
                Score = 80,
                SessionDate = DateTime.Parse("2024-03-02 12:00").ToUniversalTime()
            },
            
        ]);
    }

    private static void SeedFlashcards(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>().HasData(
            new()
            {
                Id = 1,
                StackId = 1,
                Title = "C# Basics",
                Question = "What is the purpose of the 'using' statement in C#?",
                Answer =
                    "The 'using' statement is used to ensure that IDisposable objects, such as file and database connections, are properly disposed of once they are no longer needed."
            },
            new()
            {
                Id = 2,
                StackId = 1,
                Title = "Object-Oriented Programming",
                Question = "What is polymorphism in C#?",
                Answer =
                    "Polymorphism is a concept in C# that allows methods to do different things based on the object that it is acting upon, enabling objects of different classes to be treated as objects of a common superclass."
            },
            new()
            {
                Id = 3,
                StackId = 1,
                Title = "C# Collections",
                Question = "What is the difference between List<T> and Array in C#?",
                Answer =
                    "The main difference is that arrays have a fixed size while List<T> can dynamically change size. List<T> also provides more methods for searching, sorting, and manipulating collections."
            },
            new()
            {
                Id = 4,
                StackId = 2,
                Title = "Title1",
                Question = "Q1",
                Answer = "A1"
            },
            new()
            {
                Id = 5,
                StackId = 2,
                Title = "Title2",
                Question = "Q2",
                Answer = "A2"
            },
            new()
            {
                Id = 6,
                StackId = 2,
                Title = "Title3",
                Question = "Q3",
                Answer = "A3"
            }
        );
    }

    private static void SeedStack(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stack>().HasData([
            new()
            {
                Id = 1,
                Name = "C# Stack"
            },
            new()
            {
                Id = 2,
                Name = "JavaScript Stack"
            }
        ]);
    }
}