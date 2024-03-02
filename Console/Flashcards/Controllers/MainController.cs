namespace Flashcards.Controllers;

public class MainController
{
    public void ShowMainMenu()
    {
        var deckController = new StackController();
        var flashcardController = new FlashcardController();
        var view = new MenuView();
        
        var run = true;
        while (run)
        {
            var menuOptions = new Dictionary<string, Action>
            {
                ["Create Stack"] = () => deckController.CreateStack(),
                ["Manage Stack"] = () => deckController.ManageStack(),
                ["Create Flashcard"] = () => flashcardController.CreateFlashcard(), 
                ["Exit"] = () => run = false
            };

            var choice = view.ShowMenu(menuOptions.Keys.ToArray());
            menuOptions[choice]();
        }
    }
}