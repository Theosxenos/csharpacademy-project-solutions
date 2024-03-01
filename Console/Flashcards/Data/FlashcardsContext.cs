namespace Flashcards.Data;

public class FlashcardsContext : DbContext
{
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<Stack> Decks { get; set; }

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
}