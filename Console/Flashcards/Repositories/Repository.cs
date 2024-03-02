namespace Flashcards.Repositories;

public class Repository
{
    private FlashcardsContext dbContext = new FlashcardsContext();

    public void CreateStack(Stack stack)
    {
        try
        {
            dbContext.Stacks.Add(stack);
            dbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number is 2627 or 2601)
        {
            throw new ArgumentException($"A stack with the name '{stack.Name}' already exists.");
        }
    }
}