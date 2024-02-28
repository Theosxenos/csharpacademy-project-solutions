namespace BreweryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController(IQuoteService quoteService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Quote>>> GetAllQuotes()
    {
        // TODO change to QuoteResponse
        return Ok(await quoteService.GetAllQuotes());
    }

    [HttpPost]
    public async Task<ActionResult> CreateQuote(QuoteRequest quoteRequest)
    {
        return Ok(await quoteService.CreateQuote(quoteRequest));
    }
}