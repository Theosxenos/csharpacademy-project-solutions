namespace Flashcards.Controllers;

public class PracticeController
{
    private PracticeView view = new();
    private Repository repository = new();
    public void StartSession()
    {
        var stacks = repository.GetAllStacks();
        if (stacks.Count == 0) throw new NoStacksFoundException();

        var stack = view.ShowMenu(stacks, "Select a stack to practice:");
        if (stack.Flashcards.Count == 0) throw new NoFlashcardsFoundException();
        
        StartPractice(stack);
    }
    
    private void StartPractice(Stack stack)
    {
        var toPracticeFlashcards = new List<Flashcard>(stack.Flashcards);
        var questionsAsked = 0;
        var questionsCorrect = 0;
    
        // TODO add a way out of this loop
        do
        {
            var cardToTest = toPracticeFlashcards.First();
            toPracticeFlashcards.Remove(cardToTest);
            
            view.ShowMessage($"[yellow]{cardToTest.Question}[/]");
            questionsAsked++;
            
            view.ShowMessage($"The right answer is {cardToTest.Answer}");
            var userAnswerCorrect = view.AskConfirm("Did you answer the question correctly?");

            if (userAnswerCorrect)
            {
                view.ShowMessage(
                    "Then I will not ask you again this session. Press any key to go to the next question.");
                questionsCorrect++;
            }
            else
            {
                view.ShowMessage("Then I will ask you again later this session. Press any key to go to the next question.");
                toPracticeFlashcards.Add(cardToTest);
            }
        } while (toPracticeFlashcards.Count > 0);
        
        view.ShowMessage($"During your session you were questioned {questionsAsked} times, and answered {questionsCorrect} times correctly.");

        try
        {
            var currentSession = new Session()
            {
                StackId = stack.Id,
                Score = questionsAsked - questionsCorrect,
                SessionDate = DateTime.UtcNow
            };
            repository.CreateSession(currentSession);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}