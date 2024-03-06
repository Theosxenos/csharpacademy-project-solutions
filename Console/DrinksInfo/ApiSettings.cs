namespace DrinksInfo;

public class ApiSettings
{
    private static ApiSettings instance;

    private ApiSettings(){}

    public static ApiSettings Instance => instance ??= LoadSettings();

    public string BaseUrl { get; set; }
    
    private static ApiSettings LoadSettings()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();

        var settings = new ApiSettings();
        configuration.GetSection("ApiSettings").Bind(settings);

        return settings;
    }
}