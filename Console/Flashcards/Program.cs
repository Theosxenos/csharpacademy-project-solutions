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
            .AddJsonFile("appsettings.json", false, true)
            .AddUserSecrets<Program>(true);

        Configuration = builder.Build();
    }

    private static void InitializeDatabase()
    {
        using var db = new FlashcardsContext();
        if (Configuration["EnsureDelete"] == "True")
            db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}