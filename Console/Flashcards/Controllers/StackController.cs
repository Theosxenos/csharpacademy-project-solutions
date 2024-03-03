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

    public void ManageStack()
    {
        var stacks = repository.GetAllStacks();
        var chosenStack = view.ShowMenu(stacks, "Choose a stack to manage");

        if (stacks.Count == 0)
        {
            view.ShowError("No stacks found. Please create one first.");
            return;
        }
        
        var stackManageMenuOptions = new Dictionary<string, Action>
        {
            ["Remove Flashcard(s)"] = () => RemoveFlashcards(chosenStack),
            ["Update Name"] = () => UpdateStackName(chosenStack),
            ["Delete"] = () => DeleteStack(chosenStack),
            ["Exit"] = () => { }
        };
        var choice = view.ShowMenu(stackManageMenuOptions.Keys.ToArray());
        stackManageMenuOptions[choice]();
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
        catch (ArgumentException ae)
        {
            view.ShowError(ae.Message);
        }
        // TODO
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}