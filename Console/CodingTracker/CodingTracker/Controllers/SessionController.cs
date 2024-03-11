namespace CodingTracker.Controllers;

public class SessionController
{
    SessionView view = new();
    private SessionRepository _sessionRepository = new();
    
    public void StartSession()
    {
        var result = view.PromptStartSession();
        try
        {
            new SessionRepository().CreateSession(result);
            view.ShowSuccess("Session is created.");
        }
        catch (CodingTrackerException e)
        {
            view.ShowError(e.Message);
        }
    }

    public void ListSessions()
    {
        var sessions = _sessionRepository.GetAllSessions();
        var sessionLogs = new SessionLogRepository().GetAllSessionLogs();

        view.ShowSessionTree(sessions, sessionLogs);
    }

    public void ManageSessions()
    {
        var sessions = _sessionRepository.GetAllSessions();

        if (sessions.Count == 0)
        {
            new BaseView().ShowError("No sessions found. Create a session before managing.");
            return;
        }

        var session = view.ShowMenu(sessions);

        var menu = new Dictionary<string, Action<Session>>
        {
            ["Update"] = UpdateSession,
            ["Delete"] = DeleteSession ,
            ["Exit"] = _ => { } 
        };
        var menuSelection = view.ShowMenu(menu.Keys);
        menu[menuSelection].Invoke(session);
    }

    public void UpdateSession(Session session)
    {
        var updatedDate = view.PromptStartSession();
        try
        {
            _sessionRepository.UpdateSession(new Session { Id = session.Id, Day = updatedDate });
        }
        catch (CodingTrackerException e)
        {
            view.ShowError(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void DeleteSession(Session session)
    {
        var choice = view.AskConfirm(
            $"[red]Are you sure you want to delete session: [green]{session}[/][/]");

        if (!choice) return;
        
        try
        {
            new SessionRepository().DeleteSession(session.Id);
            view.ShowSuccess($"[white]{session}[/] successfully deleted.");
        }
        catch (Exception e)
        {
            view.ShowError(e.Message);
            throw;
        }
    }
}