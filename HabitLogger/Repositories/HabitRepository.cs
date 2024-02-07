using HabitLogger.Models;

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

        var updateHabitCommand = connection.CreateCommand();
        updateHabitCommand.CommandText = "UPDATE Habits SET HabitName = $name AND Unit = $unit WHERE ID = $id";
        updateHabitCommand.Parameters.AddWithValue("id", toUpdateHabitId);
        updateHabitCommand.Parameters.AddWithValue("name", habit.HabitName);
        updateHabitCommand.Parameters.AddWithValue("unit", habit.Unit);
        updateHabitCommand.ExecuteNonQuery();
    }
}
