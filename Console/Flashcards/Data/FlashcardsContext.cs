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
            connectionString = Program.Configuration.GetConnectionString("SecretConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Stack>()
            .HasIndex(s => s.Name)
            .IsUnique();

        SeedStack(modelBuilder);
        SeedFlashcards(modelBuilder);
        SeedSessions(modelBuilder);
    }

    private void SeedSessions(ModelBuilder modelBuilder)
    {
        var sessions = new List<object>();
        var startDate = new DateTime(2024, 3, 1, 12, 0, 0); // Starting point
        var random = new Random();
        var sessionId = 1; // Starting ID

        for (var stackId = 1; stackId <= 2; stackId++) // Assuming 2 stacks
        {
            var currentDate = startDate;
            for (var day = 0; day < 10; day++) // Spread over 10 days
            {
                for (var sessionOfDay = 0; sessionOfDay < 10; sessionOfDay++) // 10 sessions per day
                {
                    sessions.Add(new
                    {
                        Id = sessionId++,
                        StackId = stackId,
                        Score = random.Next(0, 101),
                        SessionDate = currentDate
                    });
                    currentDate = currentDate.AddHours(1); // Next session an hour later
                }

                currentDate = currentDate.AddDays(day + 1).AddHours(-10); // Move to next day, reset hour offset
            }
        }

        modelBuilder.Entity<Session>().HasData(sessions.ToArray());
    }

    private static void SeedFlashcards(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>().HasData(
            new Flashcard
            {
                Id = 1,
                StackId = 1,
                Title = "C# Basics",
                Question = "What is the purpose of the 'using' statement in C#?",
                Answer =
                    "The 'using' statement is used to ensure that IDisposable objects, such as file and database connections, are properly disposed of once they are no longer needed."
            },
            new Flashcard
            {
                Id = 2,
                StackId = 1,
                Title = "Object-Oriented Programming",
                Question = "What is polymorphism in C#?",
                Answer =
                    "Polymorphism is a concept in C# that allows methods to do different things based on the object that it is acting upon, enabling objects of different classes to be treated as objects of a common superclass."
            },
            new Flashcard
            {
                Id = 3,
                StackId = 1,
                Title = "C# Collections",
                Question = "What is the difference between List<T> and Array in C#?",
                Answer =
                    "The main difference is that arrays have a fixed size while List<T> can dynamically change size. List<T> also provides more methods for searching, sorting, and manipulating collections."
            },
            new Flashcard
            {
                Id = 4,
                StackId = 2,
                Title = "JavaScript Basics",
                Question = "What is the purpose of the 'typeof' operator in JavaScript?",
                Answer =
                    "The 'typeof' operator is used to determine the type of a variable or expression in JavaScript."
            },
            new Flashcard
            {
                Id = 5,
                StackId = 2,
                Title = "JavaScript Functions",
                Question = "What is a callback function in JavaScript?",
                Answer =
                    "A callback function is a function that is passed as an argument to another function and is executed after the completion of that function."
            },
            new Flashcard
            {
                Id = 6,
                StackId = 2,
                Title = "JavaScript Arrays",
                Question = "What is the difference between 'map()' and 'forEach()' methods in JavaScript arrays?",
                Answer =
                    "'map()' returns a new array based on the result of the provided callback function, while 'forEach()' executes the provided callback function for each element without returning a new array."
            }
        );
    }

    private static void SeedStack(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stack>().HasData([
            new Stack
            {
                Id = 1,
                Name = "C# Stack"
            },
            new Stack
            {
                Id = 2,
                Name = "JavaScript Stack"
            }
        ]);
    }
}