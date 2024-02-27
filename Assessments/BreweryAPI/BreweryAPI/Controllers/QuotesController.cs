namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController(IQuoteService quoteService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllQuotes()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult> CreateQuote(QuoteRequest quoteRequest)
    {
        throw new NotImplementedException();
    }
}