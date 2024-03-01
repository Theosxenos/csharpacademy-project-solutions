using Flashcards.Controllers;

namespace Flashcards;

public class Program
{
    public static IConfigurationRoot? Configuration;

    public static void Main(string[] args)
    {
        ConfigureApplication();
        InitializeDatabase();
        StartApp();
    }

    private static void StartApp()
    {
        var mainController = new MainController();
        mainController.ShowMainMenu();
    }

    private static void ConfigureApplication()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddUserSecrets<Program>(optional: true);

        Configuration = builder.Build();
    }

    private static void InitializeDatabase()
    {
        using var db = new FlashcardsContext();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}