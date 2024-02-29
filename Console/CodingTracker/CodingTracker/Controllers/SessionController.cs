using NotImplementedException = System.NotImplementedException;

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
        
        var menu = new Dictionary<string, Action>()
        {
            {"Update",() => UpdateSession(session)},
            {"Delete",() => DeleteSession(session)},
            {"Exit", () => { } }
        };
        var menuSelection = sessionManagingView.ShowManagingMenu(session.ToString(), menu.Keys.ToArray());
        menu[menuSelection]();
    }

    public void UpdateSession(Session session)
    {
        var view = new SessionView();
        var updatedDate = view.PromptStartSession();
        var repository = new Repository();
        repository.UpdateSession(new(){Id = session.Id, Day = updatedDate});
    }

    public void DeleteSession(Session session)
    {
        var menu = new Dictionary<string, Action>()
        {
            { "Yes", () => new Repository().DeleteSession(session.Id) },
            { "No", () => { } }
        };
        //TODO TEMP!
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title($"[red]Are you sure you want to delete session: [green]{session}[/][/]")
                .AddChoices(menu.Keys.ToArray())
            );

        menu[choice]();

        if (choice == "Yes")
        {
            new BaseView().ShowSuccess($"[white]{session}[/] successfully deleted.");
        }
        
    }
}