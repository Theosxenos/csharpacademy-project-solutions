namespace DrinksInfo.Controllers;

public class DrinksController
{
    private readonly ApiSettings apiSettings = ApiSettings.Instance;
    private readonly HttpClient httpClient = new();
    private readonly DrinksView view = new();

    public async Task ShowCategoryMenu()
    {
        var response =
            await httpClient.GetFromJsonAsync<DrinksResponse>(
                $"{apiSettings.BaseUrl}list.php?c=list");

        if (response?.Drinks == null)
        {
            view.ShowError("Could not retrieve categories. Please check your connection");
            return;
        }

        var categories = response.Drinks.OrderBy(d => d.Category);
        var selectedCategory = view.ShowMenu(categories.Select(d => d.Category), "Select a category:");
        await ShowDrinksMenu(selectedCategory);
    }

    public async Task ShowDrinksMenu(string category)
    {
        var response =
            await httpClient.GetFromJsonAsync<DrinksResponse>($"{apiSettings.BaseUrl}filter.php?c={category}");

        if (response?.Drinks == null)
        {
            view.ShowError("Could not retrieve drinks. Please check your connection");
            return;
        }

        while (true)
        {
            var selectedDrink =
                view.ShowMenu(response.Drinks.OrderBy(d => d.Name).Select(d => d.Name), "Select a drink:");
            await ShowDrinkDetail(selectedDrink);
        }
    }

    public async Task ShowDrinkDetail(string drinkName)
    {
        var response =
            await httpClient.GetFromJsonAsync<DrinksResponse>($"{apiSettings.BaseUrl}search.php?s={drinkName}");

        if (response?.Drinks == null)
        {
            view.ShowError("Could not retrieve categories. Please check your connection");
            return;
        }

        var drink = response.Drinks.First();
        var ingredients = MapIngredients(drink).Ingredients;
        view.ShowDrinkDetails(drink, ingredients);
    }

    private DrinkIngredientsDto MapIngredients(Drink drink)
    {
        var dto = new DrinkIngredientsDto();
        for (var i = 1; i <= 15; i++) // Assuming up to 15 ingredients
        {
            var ingredientProp = drink.GetType().GetProperty($"strIngredient{i}");
            var measureProp = drink.GetType().GetProperty($"strMeasure{i}");

            var ingredient = ingredientProp?.GetValue(drink) as string;
            var measure = measureProp?.GetValue(drink) as string;

            if (!string.IsNullOrWhiteSpace(ingredient)) dto.Ingredients.Add(ingredient, measure ?? "By taste");
        }

        return dto;
    }
}