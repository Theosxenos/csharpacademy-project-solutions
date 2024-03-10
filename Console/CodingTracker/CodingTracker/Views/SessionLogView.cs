using CodingTracker.Validators;

namespace CodingTracker.Views;

public class SessionLogView : BaseView
{
    public SessionLog AskSessionTimes()
    {
        var errorMessage = "[red]Invalid time format. Use 24-hour format (HH:mm).[/]";
        var startTime = AskInput<string>($"At what time did you start? [grey]({Validator.TimeFormat})[/]",
            Validator.ValidateStringAsTime, errorMessage);
        var endTime = AskInput<string>($"At what time did you end? [grey]({Validator.TimeFormat})[/]",
            Validator.ValidateStringAsTime, errorMessage);

        return new SessionLog
        {
            StartTime = TimeOnly.ParseExact(startTime, Validator.TimeFormat),
            EndTime = TimeOnly.ParseExact(endTime, Validator.TimeFormat),
        };
    }
}