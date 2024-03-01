namespace CodingTracker.Repositories;

public class Repository
{
    private readonly Database db = new();

    public void CreateSession(DateOnly day)
    {
        var dayString = DateToString(day);
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
                sessions.Add(new Session
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Day = DateOnly.Parse(reader.GetString(reader.GetOrdinal("Day")))
                });
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while getting the sessions.", e);
        }

        return sessions;
    }

    public void CreateLog(SessionLog sessionLog)
    {
        if (sessionLog.EndTime < sessionLog.StartTime)
            throw new ArgumentException("Log not created due to: End Time must be greater than Start Time.",
                nameof(sessionLog));

        try
        {
            using var connection = db.GetConnection();

            var query = "INSERT INTO Logs (SessionId, StartTime, EndTime) VALUES ($1,$2,$3)";
            var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("$1", sessionLog.SessionId);
            command.Parameters.AddWithValue("$2", sessionLog.StartTime.ToString("t"));
            command.Parameters.AddWithValue("$3", sessionLog.EndTime.ToString("t"));

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while creating the log.", e);
        }
    }

    public List<SessionLog> GetAllSessionLogs()
    {
        var sessionLogs = new List<SessionLog>();
        try
        {
            using var connection = db.GetConnection();

            var query = "SELECT * FROM Logs";
            var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                sessionLogs.Add(new SessionLog
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    SessionId = reader.GetInt32(reader.GetOrdinal("SessionId")),
                    StartTime = TimeOnly.Parse(reader.GetString(reader.GetOrdinal("StartTime"))),
                    EndTime = TimeOnly.Parse(reader.GetString(reader.GetOrdinal("EndTime")))
                });
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while getting the sessions.", e);
        }

        return sessionLogs;
    }

    public void UpdateSession(Session updatedSession)
    {
        try
        {
            using var connection = db.GetConnection();
            var query = "UPDATE Sessions SET Day = $day WHERE Id = $id";
            var command = new SqliteCommand(query, connection);

            command.Parameters.AddWithValue("$day", DateToString(updatedSession.Day));
            command.Parameters.AddWithValue("$id", updatedSession.Id);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            throw new CodingTrackerException("An unexpected error occurred while getting the sessions.", e);
        }
    }

    public void DeleteSession(int sessionId)
    {
        using var connection = db.GetConnection();
        var sessionsQuery = "DELETE FROM Sessions WHERE Id = $id";
        var command = new SqliteCommand(sessionsQuery, connection);

        command.Parameters.AddWithValue("$id", sessionId);
        command.ExecuteNonQuery();
    }

    private string DateToString(DateOnly date)
    {
        return date.ToString("d-M-yy");
    }
}