namespace CodingTracker.Controllers;

public class SessionController
{
    public void StartSession()
    {
        var view = new SessionView();
        var result = view.PromptStartSession();
        try
        {
            new Repository().CreateSession(result);
            view.ShowSuccess("Session is created.");
        }
        catch (CodingTrackerException e)
        {
            view.ShowError(e.Message);
        }
    }

    public void UpdateSession()
    {

    }
}