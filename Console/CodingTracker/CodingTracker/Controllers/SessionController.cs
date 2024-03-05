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

    public void ListSessions()
    {
        var view = new SessionView();

        var repository = new Repository();
        var sessions = repository.GetAllSessions();
        var sessionLogs = repository.GetAllSessionLogs();

        view.ShowSessionTree(sessions, sessionLogs);
    }

    public void ManageSessions()
    {
        var repository = new Repository();
        var sessions = repository.GetAllSessions();

        if (sessions.Count == 0)
        {
            new BaseView().ShowError("No sessions found. Create a session before managing.");
            return;
        }

        var sessionSelectionView = new SessionSelectionView();
        var session = sessionSelectionView.Prompt(sessions);

        var sessionManagingView = new SessionManagingView();

        var menu = new Dictionary<string, Action>
        {
            { "Update", () => UpdateSession(session) },
            { "Delete", () => DeleteSession(session) },
            { "Exit", () => { } }
        };
        var menuSelection = sessionManagingView.ShowManagingMenu(session.ToString(), menu.Keys.ToArray());
        menu[menuSelection]();
    }

    public void UpdateSession(Session session)
    {
        var view = new SessionView();
        var updatedDate = view.PromptStartSession();
        var repository = new Repository();

        try
        {
            repository.UpdateSession(new Session { Id = session.Id, Day = updatedDate });
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
        var baseView = new BaseView();
        var choice = baseView.AskConfirm(
            $"[red]Are you sure you want to delete session: [green]{session}[/][/]");

        if (choice)
            try
            {
                new Repository().DeleteSession(session.Id);
                baseView.ShowSuccess($"[white]{session}[/] successfully deleted.");
            }
            catch (Exception e)
            {
                baseView.ShowError(e.Message);
                throw;
            }
    }
}