namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WholesalersController(IRepository<Wholesaler> repository)
{
    [HttpGet]
    public async Task<ListResponse<Wholesaler>> GetAllWholesalers()
    {
        return new()
        {
            Data = await repository.GetAll().ToListAsync()
        };
    }
}
