namespace BreweryAPI.Services;

public class QuoteService(IRepository<Quote> quoteRepository, IBeerRepository beerRepository, IMapper mapper) : IQuoteService
{
    public async Task<Quote> CreateQuote(QuoteRequest quoteRequest)
    {
        var quoteDetails = await Task.WhenAll(quoteRequest.QuoteDetails.Select(async qd => await CreateQuoteDetail(qd)));
        var quote = mapper.Map<Quote>(quoteRequest);
        quote.QuoteDetails = quoteDetails.ToList();
        quote.TotalPrice = quoteDetails.Sum(qd => qd.TotalPrice);
        
        return await quoteRepository.AddAsync(quote);
    }

    public Task<List<Quote>> GetAllQuotes()
    {
        return quoteRepository.GetAll().ToListAsync();
    }

    private async Task<QuoteDetail> CreateQuoteDetail(QuoteDetailRequest quoteDetailRequest)
    {
        var beer = await beerRepository.GetBeerById(quoteDetailRequest.BeerId);
        var discount = CalculateDiscount(quoteDetailRequest.Amount);
        var totalPrice = CalculateTotalPrice(quoteDetailRequest.Amount, beer.RetailPrice, discount);
        return new QuoteDetail
        {
            // Assign necessary fields here
            BeerId = beer.Id,
            Amount = quoteDetailRequest.Amount,
            Discount = (float)discount,
            TotalPrice = totalPrice
        };
    }
    
    private decimal CalculateDiscount(int amount)
    {
        return amount switch
        {
            >= 20 => 0.2m,
            >= 10 => 0.1m,
            _ => 0
        };
    }

    private decimal CalculateTotalPrice(int amount, decimal retailPrice, decimal discount)
    {
        return amount * retailPrice * (1 - discount);
    }
}