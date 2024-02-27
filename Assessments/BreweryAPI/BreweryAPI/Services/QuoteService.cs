namespace BreweryAPI.Services;

public class QuoteService(IRepository<Quote> quoteRepository, IBeerRepository beerRepository, IMapper mapper) : IQuoteService
{
    public Task<Quote> CreateQuote(QuoteRequest quoteRequest)
    {
        var quote = mapper.Map<Quote>(quoteRequest);
        quote.TotalPrice = CalculateQuoteTotalPrice(quote.QuoteDetails);
        quote.QuoteDetails.ForEach((d) => CalculateDiscount(d));
        return quoteRepository.AddAsync(quote);
    }

    public async Task<List<Quote>> GetAllQuotes()
    {
        return await quoteRepository.GetAll().ToListAsync();
    }
    
    private decimal CalculateQuoteTotalPrice(List<QuoteDetail> quoteDetails)
    {
        return quoteDetails.Sum(od => od.TotalPrice);
    }

    private decimal CalculateDiscount(int amount)
    {
        return amount switch
        {
            >= 20 => 0.2f,
            >= 10 => 0.1f,
            _ => 0
        };
    }

    private decimal CalculateTotalPrice(int amount, decimal retailPrice, decimal discount)
    {
        return amount * retailPrice * (1 - discount);
    }
}