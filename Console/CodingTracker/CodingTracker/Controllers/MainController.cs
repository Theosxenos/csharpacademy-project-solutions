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
            { "Create Session Log", sessionLogController.CreateSession },
            { "Start Session", sessionController.StartSession },
            { "List Sessions", sessionController.UpdateSession },
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
