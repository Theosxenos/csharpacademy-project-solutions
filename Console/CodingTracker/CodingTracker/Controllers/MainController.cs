namespace CodingTracker.Controllers;

public class MainController
{
    public void ShowMainMenu()
    {
        var exitMenu = false;
        var view = new MainMenuView();
        var sessionController = new SessionController();
        var sessionLogController = new SessionLogController();


        Dictionary<string, Action> menuItems = new()
        {
            { "Start Session", sessionController.StartSession },
            { "Log Session", sessionLogController.CreateSessionLog },
            { "List Sessions", sessionController.ListSessions },
            { "Manage Sessions", sessionController.ManageSessions },
            { "Exit", () => exitMenu = true }
        };
        do
        {
            exitMenu = false;

            var choice = view.ShowMenu(menuItems.Keys.ToArray());
            menuItems[choice].Invoke();

        } while (!exitMenu);
    }
}
