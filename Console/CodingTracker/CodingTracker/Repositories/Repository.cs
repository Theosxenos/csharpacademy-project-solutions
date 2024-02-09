using CodingTracker.Models;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Repositories;

public class Repository()
{
    private readonly Database db = new();

    public void CreateSession(DateOnly day)
    {
        using var connection = db.GetConnection();

        var query = "INSERT INTO Sessions (Day) VALUES (@day)";
        var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@day",day.ToString("d-M-yy"));
        command.ExecuteNonQuery();
    }
}
