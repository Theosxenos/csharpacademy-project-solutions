using DrinksInfo.Models;

namespace DrinksInfo.Controllers;

public class DrinksController
{
    private readonly HttpClient httpClient = new();
    private readonly DrinksView view = new();

    public async Task ShowCategoryMenu()
    {
        var response =
            await httpClient.GetFromJsonAsync<DrinksResponse>(
                "https://www.thecocktaildb.com/api/json/v1/1/list.php?c=list");

        if (response == null)
        {
            view.ShowError("Could not retrieve categories. Please check your connection");
            return;
        }

        var categories = response.Drinks.OrderBy(d => d.Category);
        
        var selectedCategory = view.ShowMenu(categories.Select(d => d.Category), "Select a category:");
        
        // view.ShowSuccess($"You chose {selectedCategory}");
        await ShowDrinksMenu(selectedCategory);
    }

    public async Task ShowDrinksMenu(string category)
    {
        var response =
            await httpClient.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/filter.php?c={category}");

        var selectedDrink = view.ShowMenu(response.Drinks.OrderBy(d => d.Name).Select(d => d.Name), "Select a drink:");
        await ShowDrinkDetail(selectedDrink);
    }

    public async Task ShowDrinkDetail(string drinkName)
    {
        var response =
            await httpClient.GetFromJsonAsync<DrinksResponse>($"https://www.thecocktaildb.com/api/json/v1/1/search.php?s={drinkName}");

        var drink = response?.Drinks.FirstOrDefault();
        
        AnsiConsole.MarkupLine($"[bold][red]{drink.Instructions}[/][/]");
        Console.WriteLine($"{drink.DrinkThumb}/preview");
        var imageResponse = await httpClient.GetStreamAsync($"{drink.DrinkThumb}/preview");
        var image = new CanvasImage(imageResponse);
        image.MaxWidth = 16;
        AnsiConsole.Write(image);
    }
}