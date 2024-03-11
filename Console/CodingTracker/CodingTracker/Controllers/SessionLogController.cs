using CodingTracker.Validators;

namespace CodingTracker.Controllers;

public class SessionLogController
{
    private SessionRepository sessionRepository = new();
    private SessionLogRepository repository = new();
    private SessionLogView view = new();
    
    public void CreateSessionLog()
    {
        var sessions = sessionRepository.GetAllSessions();

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
        var sessions = sessionRepository.GetAllSessions();
        
        if (sessions.Count == 0)
        {
            view.ShowError("No sessions found. Create a session before logging.");
            return;
        }

        var session = view.ShowMenu(sessions, "Choose a session to manage logs from:");

        var menu = new Dictionary<string, Action<Session>>
        {
            ["Update Log"] = UpdateLog,
            ["Delete Log"] = DeleteLog,
            ["Exit"] = _ => {}
        };

        var choice = view.ShowMenu(menu.Keys);
        menu[choice].Invoke(session);
    }

    public void DeleteLog(Session session)
    {
        var logs = repository.GetLogsBySessionId(session.Id);
        var log = view.ShowMenu(logs, "Choose a log to delete:", converter: sessionLog => new string($"{sessionLog.StartTime} - {sessionLog.EndTime}"));
        repository.DeleteSessionLog(log);
        view.ShowSuccess($"Log from {log.StartTime.ToString(Validator.TimeFormat)} deleted");
    }

    public void UpdateLog(Session session)
    {
        var logs = repository.GetLogsBySessionId(session.Id);
        var log = view.ShowMenu(logs, "Choose a log to update:",
            converter: sessionLog => new string($"{sessionLog.StartTime} - {sessionLog.EndTime}"));
        var updatedLog = view.AskSessionTimes(log);
        updatedLog.Id = log.Id;

        repository.UpdateSessionLog(updatedLog);
        view.ShowSuccess("Log has been updated");
    }
}