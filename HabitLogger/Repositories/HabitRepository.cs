using HabitLogger.Models;
using Microsoft.Data.Sqlite;

namespace HabitLogger.Repositories;

public class HabitRepository
{
    private HabitLoggerDatabase db = new();

    public List<HabitModel> GetAllHabits()
    {
        var habits = new List<HabitModel>();
        using var connection = db.GetConnection();
        var selectAllHabitsCommand = connection.CreateCommand();
        selectAllHabitsCommand.CommandText = "SELECT * FROM Habits";

        using var reader = selectAllHabitsCommand.ExecuteReader();
        while (reader.Read())
        {
            var habit = new HabitModel()
            {
                Id = reader.GetInt32(0),
                HabitName = reader.GetString(1),
                Unit = reader.GetString(2)
            };

            habits.Add(habit);
        }

        return habits;
    }

    public HabitModel GetHabitById(int habitId)
    {
        HabitModel habit = new();
        using var connection = db.GetConnection();
        var selectHabitCommand = new SqliteCommand("SELECT * FROM Habits WHERE ID = $id", connection);
        selectHabitCommand.Parameters.AddWithValue("$id", habitId);

        using var reader = selectHabitCommand.ExecuteReader();
        if (!reader.Read()) return habit;

        habit.Id = reader.GetInt32(reader.GetOrdinal("ID"));
        habit.HabitName = reader.GetString(reader.GetOrdinal("HabitName"));;
        habit.Unit = reader.GetString(reader.GetOrdinal("Unit"));;

        return habit;
    }

    public void AddHabit(HabitModel habit)
    {
        using var connection = db.GetConnection();

        var insertHabitCommand = connection.CreateCommand();
        insertHabitCommand.CommandText = "INSERT INTO Habits (HabitName, Unit) VALUES ($name, $unit)";
        insertHabitCommand.Parameters.AddWithValue("$name", habit.HabitName);
        insertHabitCommand.Parameters.AddWithValue("$unit", habit.Unit);
        insertHabitCommand.ExecuteNonQuery();
    }

    public void UpdateHabit(HabitModel habit, int toUpdateHabitId)
    {
        using var connection = db.GetConnection();

        var updateHabitCommand =
            new SqliteCommand("UPDATE Habits SET HabitName = $name, Unit = $unit WHERE ID = $id", connection);
        updateHabitCommand.Parameters.AddWithValue("$id", toUpdateHabitId);
        updateHabitCommand.Parameters.AddWithValue("$name", habit.HabitName);
        updateHabitCommand.Parameters.AddWithValue("$unit", habit.Unit);
        updateHabitCommand.ExecuteNonQuery();
    }
}
