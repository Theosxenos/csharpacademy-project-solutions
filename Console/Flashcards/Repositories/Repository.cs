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

    public List<Stack> GetAllStacks()
    {
        try
        {
            return dbContext.Stacks.Include(s => s.Flashcards).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void UpdateStack(Stack stack)
    {
        try
        {
            dbContext.Update(stack);
            dbContext.SaveChanges();
        }
        catch (DbUpdateException ex) when ((ex.InnerException as SqlException)?.Number is 2627 or 2601)
        {
            throw new ArgumentException($"A stack with the name '{stack.Name}' already exists.");
        }
    }

    public void DeleteStack(Stack stack)
    {
        try
        {
            dbContext.Remove(stack);
            dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void CreateFlashcard(Flashcard flashcard)
    {
        try
        {
            dbContext.Flashcards.Add(flashcard);
            dbContext.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}