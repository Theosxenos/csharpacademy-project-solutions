namespace BreweryAPI.Interfaces.Services;

public interface IWholesalerInventoryService
{
    public Task<InventoryItemResponse> UpsertWholesalerInventoryItem(int wholesalerId, int beerId, int amount);
}