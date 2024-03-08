using System.Globalization;
using ExerciseTracker.Models;

namespace ExerciseTracker.Views;

public class SquatsView : BaseView
{
    public Squat ShowSquatLogsMenu(List<Squat> squats)
    {
        return ShowMenu(squats, "Choose a squat to manage:", converter: squat => $"{squat.DateStart:d-M-y h:m} - {squat.Duration} - {squat.Comments}");
    }

    public void PromptUpsertSquat(Squat squat)
    {
        var prompt = "What's the {0} date/time of your squat session?";
        var errorMessage = "[red]Invalid date/time[/]";

        bool retry;
        do
        {
            retry = false;
            var dateStart = AskInput(string.Format(prompt, "start"), null,
                errorMessage, squat.DateStart);
            var dateEnd = AskInput(string.Format(prompt, "end"), null,
                errorMessage, squat.DateEnd);

            if (dateEnd < dateStart)
            {
                ShowError("End date cannot be earlier than the start date.");
                retry = AskConfirm("Retry?");
                continue;
            }

            var comments = AskInput("Write a comment on your session:",
                defaultValue: string.IsNullOrEmpty(squat.Comments) ? null : squat.Comments);

            squat.DateStart = dateStart;
            squat.DateEnd = dateEnd;
            squat.Duration = dateEnd - dateStart;
            squat.Comments = comments;

        } while (retry);
    }
}