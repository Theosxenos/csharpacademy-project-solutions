namespace Flashcards.Views;

public class FlashcardTableView : BaseView
{
    public void ShowTable(List<FlashcardDto> flashcardDtos)
    {
        var table = new Table();

        table.AddColumn("Flashcard");
        table.AddColumn("Stack");

        flashcardDtos.ForEach( f => table.AddRow(f.FlashcardTitle, f.StackName));
        
        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine("[gray]Press any key to go back[/]");
        Console.ReadKey();
    }
    
}