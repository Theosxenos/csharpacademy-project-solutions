namespace Flashcards.Controllers;

public class FlashcardController
{
    public void CreateFlashcard()
    {
        var createFlashcardView = new CreateFlashcardView();
        var repository = new Repository();
        var stacks = repository.GetAllStacks();
        
        if (stacks.Count == 0)
        {
            createFlashcardView.ShowError("No stacks found. Please create one first.");
            return;
        }
        
        var menuView = new MenuView();
        var stack = menuView.ShowMenu(stacks, "Choose a stack to place the card in:");

        var continueAdding = true;
        while (continueAdding)
        {
            try
            {
                var flashcard = createFlashcardView.Prompt();
                flashcard.Stack = stack;
                repository.CreateFlashcard(flashcard);
                createFlashcardView.ShowSuccess("Flashcard created successfully.");
                continueAdding = createFlashcardView.AskConfirm("Do you want to add another flashcard? (yes/no)");
            }
            catch (Exception e)
            {
                createFlashcardView.ShowError(e.Message);
                continueAdding = false;
            }
        }
    }
}