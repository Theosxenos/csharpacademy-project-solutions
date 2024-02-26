namespace BreweryAPI.Services;

public class WholesalerInventoryService(IRepository<InventoryItem> repository, IMapper mapper) : IWholesalerInventoryService
{
    public async Task<InventoryItemResponse> UpsertWholesalerInventoryItem(int wholesalerId, int beerId, int amount)
    {
        var inventoryItem = await repository.GetAll()
            .FirstOrDefaultAsync(i => i.BeerId == beerId && i.WholesalerId == wholesalerId);
        
        InventoryItem inventory;
        if (inventoryItem != null)
        {
            inventoryItem.Amount += amount;
            inventory = await repository.UpdateAsync(inventoryItem);
        }
        else
        {
            inventory = await repository.AddAsync(new()
            {
                BeerId = beerId,
                WholesalerId = wholesalerId,
                Amount = amount
            });
        }

        return mapper.Map<InventoryItemResponse>(inventory);
    }
}