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

    public void DeleteFlashcard(Flashcard flashcard)
    {
        try
        {
            dbContext.Flashcards.Remove(flashcard);
            dbContext.SaveChanges();
            
            ResetFlashcardIdNumbering();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }        
    }

    public void DeleteFlashcard(List<Flashcard> flashcards)
    {
        try
        {
            dbContext.Flashcards.RemoveRange(flashcards);
            dbContext.SaveChanges();
            
            ResetFlashcardIdNumbering();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void ResetFlashcardIdNumbering()
    {
        var allCards = dbContext.Flashcards.OrderBy(f => f.Id).Select(f => new Flashcard
        {
            StackId = f.StackId,
            Title = f.Title,
            Question = f.Question,
            Answer = f.Answer
        }).ToList();
            
        dbContext.Flashcards.RemoveRange(dbContext.Flashcards);
        dbContext.SaveChanges();
        dbContext.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Flashcards', RESEED, 0);");
        dbContext.Flashcards.AddRange(allCards);
        dbContext.SaveChanges();
    }
}