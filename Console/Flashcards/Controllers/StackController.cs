using Flashcards.Repositories;

namespace Flashcards.Controllers;

public class StackController
{
    public void CreateStack()
    {
        var baseView = new BaseView();
        var retry = false;
        var repository = new Repository();
        do
        {
            try
            {
                var view = new CreateStackView();
                var stackName = view.Prompt();
                repository.CreateStack(new() { Name = stackName });
                baseView.ShowSuccess($"Stack '{stackName}' has been created.");
            }
            catch (Exception e)
            {
                baseView.ShowError(e.Message);
                retry = baseView.AskConfirm("Would you like to try again?");
            }
        } while (retry);
    }
}