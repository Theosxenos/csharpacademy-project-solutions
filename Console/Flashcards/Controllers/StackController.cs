namespace Flashcards.Controllers;

public class StackController
{
    private Repository repository = new();
    private StackView view = new();
    
    public void CreateStack()
    {
        var view = new StackView();
        var retry = false;
        do
        {
            try
            {
                var stackName = view.CreateStack();
                repository.CreateStack(new() { Name = stackName });
                view.ShowSuccess($"Stack '{stackName}' has been created.");
            }
            catch (Exception e)
            {
                view.ShowError(e.Message);
                retry = view.AskConfirm("Would you like to try again?");
            }
        } while (retry);
    }

    /// <summary>
    /// First it shows the user a list of stacks.
    /// After the user chooses a stack it shows some menu options related to managing it.
    /// </summary>
    /// <exception cref="NoStacksFoundException">Thrown if no stacks are found</exception>
    public void ManageStack()
    {
        var stacks = repository.GetAllStacks();
        if (stacks.Count == 0) throw new NoStacksFoundException();

        var showMenu = true;
        var stackManageMenuOptions = new Dictionary<string, Action<Stack>>
        {
            ["Remove Flashcard(s)"] = RemoveFlashcards,
            ["Update Name"] = UpdateStackName,
            ["Delete"] = (stack) =>
            {
                DeleteStack(stack);
                showMenu = false;
            },
            ["Exit"] = _ => showMenu = false
        };

        var chosenStack = view.ShowMenu(stacks, "Choose a stack to manage");

        while (showMenu)
        {
            try
            {
                var choice = view.ShowMenu(stackManageMenuOptions.Keys.ToArray());
                stackManageMenuOptions[choice](chosenStack);
            }
            catch (NotFoundException e)
            {
                view.ShowError(e.Message);
            }
        }
    }

    public void DeleteStack(Stack chosenStack)
    {
        var view = new BaseView();
        var confirmation = view.AskConfirm($"Are you sure you want to delete '{chosenStack.Name}'?");
        if(!confirmation) return;

        try
        {
            repository.DeleteStack(chosenStack);
        }
        catch (Exception e)
        {
            view.ShowError(e.Message);
        }
    }

    public void RemoveFlashcards(Stack chosenStack)
    {
        if (chosenStack.Flashcards.Count == 0) throw new NoFlashcardsFoundException();
            
        var selection = view.RemoveFlashcards(chosenStack.Flashcards);
        var selectionCount = selection.Count;
        switch (selectionCount)
        {
            case 0:
                view.ShowMessage("No cards have been selected. Returning to menu.");
                return;
            case >= 3 when !view.AskConfirm($"[red]You are about to delete {selectionCount} cards, are you sure?[/]"):
                return;
        }
        
        try
        {
            repository.DeleteFlashcard(selection);
            view.ShowSuccess($"{selectionCount} Flashcards have been deleted");
        }
        catch (Exception e)
        {
            view.ShowError(e.Message);
        }
    }

    public void UpdateStackName(Stack stack)
    {
        try
        {
            stack.Name = view.AskInput("What should the new name of the stack be?");
            repository.UpdateStack(stack);
        }
        catch (ArgumentException e)
        {
            view.ShowError(e.Message);
        }
    }
}