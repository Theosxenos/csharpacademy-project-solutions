namespace Flashcards.Controllers;

public class FlashcardController
{
    private MenuView menuView = new();
    private Repository repository = new();
    
    public void CreateFlashcard()
    {
        var createFlashcardView = new UpsertFlashcardView();
        try
        {
            var stack = GetStackFromMenu("Choose a stack to place the card in:");

            var continueAdding = true;
            while (continueAdding)
            {
                var flashcard = createFlashcardView.CreateFlashcard();
                flashcard.Stack = stack;
                repository.CreateFlashcard(flashcard);
                createFlashcardView.ShowSuccess("Flashcard created successfully.");
                continueAdding = createFlashcardView.AskConfirm("Do you want to add another flashcard?");
            }
        }
        catch (NotFoundException e)
        {
            createFlashcardView.ShowError(e.Message);
        }
    }

    public void ListFlashcards()
    {
        var flashcardTableView = new FlashcardTableView();
        
        try
        {
            var stack = GetStackFromMenu();
            var dtoList = stack.Flashcards.Select(f => new FlashcardDto(f.Title, stack.Name)).ToList();

            flashcardTableView.ShowTable(dtoList);
        }
        catch (NotFoundException e)
        {
            flashcardTableView.ShowError(e.Message);
        }
    }

    public void UpdateFlashcard()
    {
        var stack = GetStackFromMenu();
        var toUpdateFlashcard = menuView.ShowMenu(stack.Flashcards, "Choose a flashcard to update");
        
        var view = new UpsertFlashcardView();
        view.UpdateFlashcard(toUpdateFlashcard);

        try
        {
            repository.UpdateFlashcard(toUpdateFlashcard);
        }
        catch (NotFoundException e)
        {
            view.ShowError(e.Message);
        }
    }

    private Stack GetStackFromMenu(string menuTitle = "Choose a stack to list the cards from")
    {
        var stacks = repository.GetAllStacks();
        if (stacks.Count == 0)
        {
            throw new NoStacksFoundException();
        }

        return menuView.ShowMenu(stacks, menuTitle);
    }
}