using HabitLogger.Models;

namespace HabitLogger.Repositories;

public class HabitLogRepository
{
    private HabitLoggerDatabase db = new();

    public List<HabitLogModel> GetAllLogsForHabitId(int habitId)
    {
        var logs = new List<HabitLogModel>();
        using var connection = db.GetConnection();
        var selectLogsCommand = connection.CreateCommand();
        selectLogsCommand.CommandText = "SELECT * FROM HabitLogs WHERE HabitID = $habitid";
        selectLogsCommand.Parameters.AddWithValue("$habitid", habitId);

        using var reader = selectLogsCommand.ExecuteReader();
        while (reader.Read())
        {
            var habitLog = new HabitLogModel()
            {
                LogId = reader.GetInt32(0),
                HabitId = reader.GetInt32(1),
                Quantity = reader.GetInt32(2),
                Date = DateTime.Parse(reader.GetString(3))
            };

            logs.Add(habitLog);
        }

        return logs;
    }
}
