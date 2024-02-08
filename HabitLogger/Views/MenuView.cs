namespace HabitLogger.Views;

public class MenuView
{
    public string Title { get; set; } = string.Empty;

    public int ShowMenu(IList<string> menuItems)
    {
        var pageNumber = 0;
        var pageSize = 8;

        while (true)
        {
            DisplayMenu(menuItems, pageNumber, pageSize);
            HandleUserInput();
        }
    }

    private void HandleUserInput()
    {
        var userInput = GetUserInput();
        var isUserChoiceNumeric = int.TryParse(userInput, out var menuChoice);

        if (isUserChoiceNumeric)
        {
            switch (menuChoice)
            {
                case >= 1 and <= 9:
                    return pageNumber * pageSize + menuChoice;
                case 0:
                    return 0;
            }
        }
        else if (!isUserChoiceNumeric)
        {
            HandlePagination(userInput);
        }
    }

    private static void HandlePagination(string userInput)
    {
        switch (userInput)
        {
            case "p" when pageNumber > 0:
                pageNumber--;
                break;
            case "p" when pageNumber == 0:
                Console.WriteLine("You already are on the first page.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                break;
            case "n" when pageNumber < totalPages:
                pageNumber++;
                break;
            case "n" when pageNumber == totalPages:
                Console.WriteLine("You already are on the last page.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
                break;
        }
    }

    private void DisplayMenu(IList<string> menuItems, int pageNumber, int pageSize)
    {
        Console.Clear();
        Console.WriteLine(Title);
        Console.WriteLine(new string('-', Title.Length));

        var amountOfItemsToList = Math.Min(pageSize, menuItems.Count - pageNumber * pageSize);
        for (var i = 0; i < amountOfItemsToList; i++)
        {
            var index = pageNumber * pageSize + i;
            Console.WriteLine($"{i + 1}. {menuItems[index]}");
        }
        Console.WriteLine("0. Back to previous menu.");

        if ((int)Math.Ceiling(menuItems.Count / (double)pageSize) > 1)
        {
            Console.WriteLine("N. Next Page, P. Previous Page");
        }

        Console.WriteLine("\nYour Choice: ");
    }

    private string GetUserInput()
    {
        while (true)
        {
            var userInput = Console.ReadLine()?.ToLower().Trim();

            if (!string.IsNullOrEmpty(userInput)) return userInput;

            Console.WriteLine("Please enter a value.");
            Console.WriteLine("Press any key to try again.");
            Console.ReadKey();
        }
    }
}
