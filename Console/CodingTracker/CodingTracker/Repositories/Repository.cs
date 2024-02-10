using CodingTracker.Models;
using Microsoft.Data.Sqlite;

namespace CodingTracker.Repositories;

public class Repository()
{
    private readonly Database db = new();

    public void CreateSession(DateOnly day)
    {
        var dayString = day.ToString("d-M-yy");
        try
        {
            using var connection = db.GetConnection();

            var query = "INSERT INTO Sessions (Day) VALUES (@day)";
            var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@day", dayString);
            command.ExecuteNonQuery();
        }
        catch (SqliteException e) when (e.SqliteErrorCode == 19)
        {
            throw new CodingTrackerException($"A session with the date {dayString} already exists.", e);
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while creating the session.", e);
        }
    }

    public List<Session> GetAllSessions()
    {
        var sessions = new List<Session>();
        try
        {
            using var connection = db.GetConnection();

            var query = "SELECT * FROM Sessions";
            var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sessions.Add(new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Day = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Day")))
                });
            }
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while getting the sessions.", e);
        }

        return sessions;
    }

    public void CreateLog(SessionLog sessionLog)
    {
        throw new NotImplementedException();
        try
        {
            using var connection = db.GetConnection();

            var query = "INSERT INTO Logs ()";
            var command = new SqliteCommand(query, connection);
            // command.Parameters.AddWithValue("");
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while creating the log.", e);
        }
    }
}
