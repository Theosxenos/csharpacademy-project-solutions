namespace CodingTracker.Controllers;

public class SessionLogController
{
    Repository repository = new();
    private SessionLogView view = new();
    
    public void CreateSessionLog()
    {
        var sessions = repository.GetAllSessions();

        if (sessions.Count == 0)
        {
            view.ShowError("No sessions found. Create a session before logging.");
            return;
        }
        
        var session = view.ShowMenu(sessions);
        var sessionLog = view.AskSessionTimes();
        sessionLog.SessionId = session.Id;

        try
        {
            repository.CreateLog(sessionLog);
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }

    public void ManageLogs()
    {
        var sessions = repository.GetAllSessions();
        
        if (sessions.Count == 0)
        {
            view.ShowError("No sessions found. Create a session before logging.");
            return;
        }

        var session = view.ShowMenu(sessions, "Choose a session to manage logs from:");
        var logs = repository.GetLogsBySessionId(session.Id);

        var menu = new Dictionary<string, Action>
        {

        };
    }
}