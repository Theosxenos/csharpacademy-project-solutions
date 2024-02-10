using Microsoft.Data.Sqlite;

namespace CodingTracker.Data;

public class Database
{
    private Configuration appConfiguration;
    private string connectionString;
    private string databasePath;

    public Database()
    {
        appConfiguration = new();
        databasePath = appConfiguration.GetConfigurationItemByKey("DatabasePath");
        var configConnectionString = appConfiguration.GetConfigurationItemByKey("ConnectionString");
        connectionString = configConnectionString + databasePath;
    }

    public void Initialize()
    {
        if (File.Exists(databasePath)) return;

        SeedDatabase();
    }

    public SqliteConnection GetConnection()
    {
        var connection = new SqliteConnection(connectionString);

        try
        {
            connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return connection;
    }

    private void SeedDatabase()
    {
        using var connection = GetConnection();

        var createTableQuery =
            """
                CREATE TABLE IF NOT EXISTS Sessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Day TEXT NOT NULL UNIQUE 
                );
            
                CREATE TABLE IF NOT EXISTS Logs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    SessionId INTEGER NOT NULL,
                    StartTime TEXT,
                    EndTime TEXT,
                    FOREIGN KEY (SessionId) REFERENCES Sessions(Id)
                );
            """;

        var createTableCommand = new SqliteCommand(createTableQuery, connection);
        createTableCommand.ExecuteNonQuery();

        var seedDataQuery =
            """
                -- Seed sessions
                INSERT INTO Sessions (Day) VALUES
                ('1-1-24'),
                ('2-1-24'),
                ('10-2-24');
                
                -- Seed logs
                INSERT INTO Logs (SessionId, StartTime, EndTime) VALUES
                (1, '09:00', '10:00'),
                (1, '10:00', '11:00'),
                (2, '11:00', '12:00'),
                (2, '12:00', '13:00'),
                (3, '13:00', '14:00');
            """;

        var seedDataCommand = new SqliteCommand(seedDataQuery, connection);
        seedDataCommand.ExecuteNonQuery();
    }

}