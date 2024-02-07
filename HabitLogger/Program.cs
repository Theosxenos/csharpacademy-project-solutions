﻿using HabitLogger;
using HabitLogger.Models;
using HabitLogger.Repositories;

if (!File.Exists("habits.db"))
{
    var db = new HabitLoggerDatabase();
    db.CreateDatabase();
}

var endApp = false;
while (!endApp)
{
    Console.Clear();
    Console.WriteLine("Habit Logger Main Menu");
    Console.WriteLine("1. New Habit");
    Console.WriteLine("2. List Habits");
    Console.WriteLine("0. Exit Habit Logger");
    Console.Write("Your choice? ");

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
        case 2:
            ListHabitsView();
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

            Console.WriteLine($"Your habit {habitName} has been created! Press a key to return to the main menu.");
            Console.ReadKey();

            endHabitView = true;
        }
    } while (!endHabitView);
}

static void ListHabitsView()
{
    var habits = new HabitRepository().GetAllHabits();
    bool endListHabitsView;

    do
    {
        Console.Clear();
        Console.WriteLine("A List of All Your Habits");

        for (int i = 0; i < habits.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {habits[i].HabitName}");
        }

        Console.WriteLine("0. Back to main menu");
        Console.Write("Your choice? ");
        var isUserChoiceNumeric = int.TryParse(Console.ReadLine(), out var userChoice);

        if (isUserChoiceNumeric)
        {
            endListHabitsView = false;

            switch (userChoice)
            {
                case >= 1 when userChoice <= habits.Count:
                    HabitMenuView(habits[userChoice - 1]);
                    break;
                case 0:
                    endListHabitsView = true;
                    break;
            }
        }
        else
        {
            Console.WriteLine("Wrong input. Press a key to try again.");
            Console.ReadKey();

            endListHabitsView = false;
        }

    } while (!endListHabitsView);
}

static void HabitMenuView(HabitModel habit)
{
    bool endHabitMenuView;
    do
    {
        Console.WriteLine($"{habit.HabitName} Menu");
        Console.WriteLine("1. Show Log");
        Console.WriteLine("2. Update Log");
        Console.WriteLine("3. Update Habit");
        Console.WriteLine("4. Stats");
        Console.WriteLine("0. Habit Menu");
        Console.Write("Your choice? ");
        var isUserChoiceNumeric = int.TryParse(Console.ReadLine(), out var userChoice);

        if (isUserChoiceNumeric && userChoice is >= 0 and <= 4)
        {
            endHabitMenuView = false;

            switch (userChoice)
            {
                case 1:
                    HabitLogView(habit);
                    break;
                case 2:
                    break;
                case 3:
                    HabitUpdateView(habit);
                    break;
                case 4:
                    break;
                case 0:
                    endHabitMenuView = true;
                    break;
            }

        }
        else
        {
            Console.WriteLine("Wrong input. Press a key to try again.");
            Console.ReadKey();

            endHabitMenuView = false;
        }

    } while (!endHabitMenuView);
}

static void HabitLogView(HabitModel habit)
{
    var habitLogRepository = new HabitLogRepository();
    var logs = habitLogRepository.GetAllLogsForHabitId(habit.Id);

    Console.WriteLine($"{habit.HabitName} Logs");

    if (logs.Count == 0)
    {
        Console.WriteLine("No logs found.");
    }

    foreach (var log in logs)
    {
        Console.WriteLine($"{log.Date} - {log.Quantity} {habit.Unit}");
    }

    Console.WriteLine("Press any key to return to the habit menu.");
    Console.ReadKey();
}

static void updateLogView(HabitModel habit)
{
    Console.WriteLine();
}

static void HabitUpdateView(HabitModel habit)
{
    Console.WriteLine($"Updating Habit: {habit.HabitName}");
    Console.WriteLine("Keep the values empty to keep the current value.");
    Console.WriteLine($"Update the name ({habit.HabitName})?");
    var habitName = Console.ReadLine();
    Console.WriteLine($"Update the unit ({habit.Unit})?");
    var habitUnit = Console.ReadLine();

    var updatedHabit = new HabitModel
    {
        HabitName = string.IsNullOrEmpty(habitName) ? habit.HabitName : habitName,
        Unit = string.IsNullOrEmpty(habitUnit) ? habit.Unit : habitUnit
    };

    var habitRepository = new HabitRepository();
    habitRepository.UpdateHabit(updatedHabit, habit.Id);
}
