namespace DrinksInfo.Views;

public class DrinksView : BaseView
{
    public void ShowDrinkDetails(Drink drink, Dictionary<string, string> ingredients)
    {
        var recipe = string.Join(", ", ingredients.Select(x => $"{x.Key}: {x.Value.Trim()}"));

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();
        grid.AddColumn();

        var leftSubGrid = new Grid();
        leftSubGrid.AddColumn();
        leftSubGrid.AddColumn();

        leftSubGrid.AddRow(["Name:", drink.Name]);
        leftSubGrid.AddRow(["Category:", drink.Category]);
        leftSubGrid.AddRow(["Alcoholic:", drink.Alcoholic]);
        leftSubGrid.AddRow(["Glass:", drink.Glass]);


        var rightSubgrid = new Grid();
        rightSubgrid.AddColumn();
        rightSubgrid.AddColumn();

        rightSubgrid.AddRow(["IBA:", drink.Iba ?? "None"]);
        rightSubgrid.AddRow(["Ingredients:", recipe]);
        rightSubgrid.AddRow(["Instructions:", drink.Instructions]);

        grid.AddRow([leftSubGrid, new Markup("|\n|\n|\n|\n"), rightSubgrid]);

        AnsiConsole.Write(grid);

        AnsiConsole.MarkupLine("[grey]Press any key to go back.[/]");
        Console.ReadKey();
    }
}