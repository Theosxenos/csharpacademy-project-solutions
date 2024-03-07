using ShiftsLogger.Shared.Models;
using Spectre.Console;

namespace ShiftsLogger.Client.Views;

public class MainView : BaseView
{
    public DateTime AskStartShift()
    {
        return AskShift("What's the start datetime of the shift?");
    }

    public DateTime AskEndShift()
    {
        return AskShift("What's the end datetime of the shift?");
    }

    private DateTime AskShift(string prompt)
    {
        var date = AskInput<string>(prompt, i => DateTime.TryParse(i, out _), "Not a valid datetime");

        return DateTime.Parse(date);
    }

    public void ListShifts(List<Shift> shifts)
    {
        var table = new Table();
        table.AddColumn("Date");
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Duration");

        foreach (var shift in shifts)
        {
            var date = shift.StartShift.ToString("d-M-y");
            var start = shift.StartShift.ToString("HH:mm");
            var end = shift.EndShift.Day > shift.StartShift.Day
                ? shift.EndShift.ToString("d-M-y HH:mm")
                : shift.EndShift.ToString("HH:mm");
            var duration = $"{shift.MinutesDuration / 60:N1} hours";

            table.AddRow([date, start, end, duration]);
        }
        
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[gray]Press any key to go back[/]");
        Console.ReadKey();
    }
}