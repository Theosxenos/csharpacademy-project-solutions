using Microsoft.Extensions.Configuration;
using ShiftsLogger.Client.Controllers;

internal class Program
{
    public static string BaseUrl { get; set; }

    public static async Task Main()
    {
        InitConfiguration();
        await new MainController().ShowMenu();
    }

    private static void InitConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        var configuration = builder.Build();
        BaseUrl = configuration["BaseUrl"] ?? throw new Exception("No BaseUrl property found in config file.");
    }
}