using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Phonebook.Models;

namespace Phonebook.Data;

public class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    
    private IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddUserSecrets<Program>().Build();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            connectionString = configuration.GetConnectionString("SecretConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>().HasData(
            new Contact
            {
                Id = 1,
                Email = "mail@example.com",
                Name = "Dr. Lipsum",
                PhoneNumber = "06-12345678"
            },
            new Contact
            {
                Id = 2,
                Email = "my.mail@example.com",
                Name = "Ingrid Jansen",
                PhoneNumber = "0123-567890"
            }
        );
    }
}