namespace CodingTracker.Controllers;

public class SessionLogController
{
    public void CreateSessionLog()
    {
        var repository = new Repository();
        var sessions = repository.GetAllSessions();

        if (sessions.Count == 0)
        {
            new BaseView().ShowError("No sessions found. Create a session before logging.");
            return;
        }
        
        var createSessionLogView = new CreateSessionLogView();
        
        var sessionListView = new CreateSessionView();
        var session = sessionListView.Prompt(sessions);
        
        var result = createSessionLogView.Prompt();
        result.SessionId = session.Id;
        
        repository.CreateLog(result);
    }
}