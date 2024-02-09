using Microsoft.Data.Sqlite;

namespace CodingTracker.Controllers;

public class MainController
{
    public void ShowMainMenu()
    {
        var view = new MainMenuView();
        var sessionController = new SessionController();
        string[] menuItems = ["Start Session", "Update Session", "List Sessions", "Exit"];
        var choice = view.ShowMenu(menuItems);

        switch (choice)
        {
            case "Start Session":
                sessionController.StartSession();
                break;
            case "Update Session":
                sessionController.UpdateSession();
                break;
        }
    }
}

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
        catch (SqliteException sqliteException)
        {
            if (sqliteException.SqliteErrorCode == 19)
            {
                view.ShowError($"A session with the date {result} already exists.");
            }
            else
            {
                throw;
            }
        }
        catch (Exception e)
        {
            view.ShowError(e.Message);
            throw;
        }
    }

    public void UpdateSession()
    {

    }
}
