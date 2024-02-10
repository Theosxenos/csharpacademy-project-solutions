namespace CodingTracker.Controllers;

public class SessionLogController
{
    public void CreateSession()
    {
        var repository = new Repository();
        var view = new CreateSessionLogView();
        var result = view.Prompt();
    }
}
