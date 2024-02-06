using HabitLogger;
using HabitLogger.Repositories;

if (!File.Exists("habits.db"))
{
    var db = new HabitLoggerDatabase();
    db.CreateDatabase();
}

var endApp = false;
// var habitRepository = new HabitRepository();

while (!endApp)
{
    Console.Clear();
    Console.WriteLine("Habit Logger Main Menu");
    Console.WriteLine("1. New Habit");
    Console.WriteLine("2. List Habits");
    Console.WriteLine("0. Exit Habit Logger");
    Console.Write("Your option? ");

    var isUserChoiceNumeric = int.TryParse(Console.ReadLine(), out var userChoice);
    if (!isUserChoiceNumeric || userChoice is >= 3 and <= 9)
    {
        Console.WriteLine("Wrong input. Press any key to try again.");
        Console.ReadKey();
    }

    switch (userChoice)
    {
        case 1:
            NewHabitView();
            break;
        case 0:
            endApp = true;
            break;
    }
}

return;

static void NewHabitView()
{
    bool endHabitView;
    do
    {
        Console.Clear();
        Console.WriteLine("Create a New Habit");
        Console.WriteLine("Enter the name of the habit:");
        var habitName = Console.ReadLine();
        Console.WriteLine("Enter a unit for measurement of the habit:");
        var habitUnit = Console.ReadLine();

        if (string.IsNullOrEmpty(habitName?.Trim()) || string.IsNullOrEmpty(habitUnit?.Trim()))
        {
            Console.WriteLine("Wrong input for either name or unit. Both fields require at least 1 character.");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
            endHabitView = false;
        }
        else
        {
            var habitRepository = new HabitRepository();
            habitRepository.AddHabit(new(){HabitName = habitName, Unit = habitUnit});

            Console.WriteLine($"Your habit ${habitName} has been created! Press a key to return to the main menu.");
            Console.ReadKey();

            endHabitView = true;
        }
    } while (!endHabitView);
}
