namespace BreweryAPI.Interfaces.Services;

public interface IQuoteService
{
    Task<Quote> CreateQuote(QuoteRequest quoteRequest);
    Task<List<Quote>> GetAllQuotes();
}