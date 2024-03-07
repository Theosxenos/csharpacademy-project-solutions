using ExerciseTracker.Models;

namespace ExerciseTracker.Views;

public class SquatsView : BaseView
{
    public Squat ShowSquatLogsMenu(List<Squat> squats)
    {
        return ShowMenu(squats, "Choose a squat to manage:", converter: squat => $"{squat.DateStart:d-M-y h:m} - {squat.Duration} - {squat.Comments}");
    }
}