using HabitLogger.Models;
using Microsoft.Data.Sqlite;

namespace HabitLogger.Repositories;

public class HabitLogRepository
{
    private HabitLoggerDatabase db = new();

    public List<HabitLogModel> GetAllLogsForHabitId(int habitId)
    {
        var logs = new List<HabitLogModel>();
        using var connection = db.GetConnection();
        var selectLogsCommand = new SqliteCommand("SELECT * FROM HabitLogs WHERE HabitID = $habitid", connection);
        selectLogsCommand.Parameters.AddWithValue("$habitid", habitId);

        using var reader = selectLogsCommand.ExecuteReader();
        while (reader.Read())
        {
            var habitLog = new HabitLogModel()
            {
                LogId = reader.GetInt32(reader.GetOrdinal("LogId")),
                HabitId = reader.GetInt32(reader.GetOrdinal("HabitId")),
                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                Date = reader.GetDateTime(reader.GetOrdinal("Date"))
            };

            logs.Add(habitLog);
        }

        return logs;
    }
}
