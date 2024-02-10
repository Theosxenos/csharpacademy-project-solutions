namespace CodingTracker.Controllers;

public class MainController
{
    public void ShowMainMenu()
    {
        var exitMenu = false;
        var view = new MainMenuView();
        var sessionController = new SessionController();
        // TODO use MenuItems
        string[] menuItems = ["Start Session", "Update Session", "List Sessions", "Exit"];

        do
        {
            exitMenu = false;

            var choice = view.ShowMenu(menuItems);
            switch (choice)
            {
                case "Start Session":
                    sessionController.StartSession();
                    break;
                case "Update Session":
                    sessionController.UpdateSession();
                    break;
                case "Exit":
                    exitMenu = true;
                    break;
            }
        } while (!exitMenu);
    }
}
