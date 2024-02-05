using HabitLogger;

if (!File.Exists("habits.db"))
{
    HabitLoggerDatabase.CreateDatabase();
}

var endApp = false;

while (!endApp)
{
    int.TryParse(Console.ReadLine(), out var userChoice);

    switch (userChoice)
    {
        case 6:
            endApp = true;
            break;
    }
}
