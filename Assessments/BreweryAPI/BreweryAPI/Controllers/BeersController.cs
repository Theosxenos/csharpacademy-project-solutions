
namespace BreweryAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BeersController : ControllerBase
{
    [HttpGet]
    public ListResponse<Beer> GetBeers()
    {
        List<Beer> beers =
        [
            new()
            {
                Id = 1,
                Name = "Plzen"
            },
            new()
            {
                Id = 2,
                Name = "Radegast"
            },
        ];

        return new()
        {
            Data = beers
        };
    }
}
