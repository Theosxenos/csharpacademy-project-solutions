namespace BreweryAPI.Data;

public class BreweryDbContext(DbContextOptions<BreweryDbContext> options) : DbContext(options)
{
    public DbSet<Beer> Beers { get; set; }
    public DbSet<Brewery> Breweries { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<QuoteDetail> QuoteDetails { get; set; }
    public DbSet<Wholesaler> Wholesalers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureBrewery(modelBuilder);
        ConfigureBeer(modelBuilder);
        ConfigureWholesalers(modelBuilder);
        ConfigureInventoryItem(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureInventoryItem(ModelBuilder modelBuilder)
    {
        // TODO
    }

    private void ConfigureWholesalers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wholesaler>().HasData([
            new Wholesaler
            {
                Id = 1,
                Name = "Global Trade Alliance Ltd."
            },
            new Wholesaler
            {
                Id = 2,
                Name = "International MegaMart Inc."
            },
            new Wholesaler
            {
                Id = 3,
                Name = "Universal Distribution Network Corp."
            },
            new Wholesaler
            {
                Id = 4,
                Name = "Worldwide Supply Solutions LLC."
            },
            new Wholesaler
            {
                Id = 5,
                Name = "Global Reach Enterprises Ltd."
            }
        ]);
    }

    private void ConfigureBeer(ModelBuilder modelBuilder)
    {
        // Seed data for beers from these breweries
        modelBuilder.Entity<Beer>().HasData(
            new Beer
            {
                Id = 1, BreweryId = 1, Name = "Heineken Lager Beer", WholesalePrice = 0.50m, RetailPrice = 1.00m
            },
            new Beer
            {
                Id = 2, BreweryId = 1, Name = "Heineken Dark Lager", WholesalePrice = 0.60m, RetailPrice = 1.20m
            },
            new Beer { Id = 3, BreweryId = 2, Name = "Amstel Light", WholesalePrice = 0.45m, RetailPrice = 0.90m },
            new Beer { Id = 4, BreweryId = 2, Name = "Amstel Radler", WholesalePrice = 0.55m, RetailPrice = 1.10m },
            new Beer { Id = 5, BreweryId = 3, Name = "Zatte", WholesalePrice = 0.70m, RetailPrice = 1.40m },
            new Beer { Id = 6, BreweryId = 3, Name = "Natte", WholesalePrice = 0.65m, RetailPrice = 1.30m },
            new Beer { Id = 7, BreweryId = 4, Name = "Budweiser", WholesalePrice = 0.50m, RetailPrice = 1.00m },
            new Beer { Id = 8, BreweryId = 5, Name = "Coors Light", WholesalePrice = 0.48m, RetailPrice = 0.96m },
            new Beer { Id = 9, BreweryId = 6, Name = "Guinness Draught", WholesalePrice = 0.75m, RetailPrice = 1.50m },
            new Beer { Id = 10, BreweryId = 7, Name = "Stella Artois", WholesalePrice = 0.52m, RetailPrice = 1.04m },
            new Beer { Id = 11, BreweryId = 8, Name = "Corona Extra", WholesalePrice = 0.55m, RetailPrice = 1.10m },
            new Beer { Id = 12, BreweryId = 9, Name = "Pilsner Urquell", WholesalePrice = 0.58m, RetailPrice = 1.16m },
            new Beer
            {
                Id = 13, BreweryId = 10, Name = "Hoegaarden Original White Ale", WholesalePrice = 0.60m,
                RetailPrice = 1.20m
            }
        );
    }

    private void ConfigureBrewery(ModelBuilder modelBuilder)
    {
        // Seed data for breweries, including more global ones
        modelBuilder.Entity<Brewery>().HasData(
            new Brewery { Id = 1, Name = "Heineken" },
            new Brewery { Id = 2, Name = "Amstel" },
            new Brewery { Id = 3, Name = "Brouwerij 't IJ" },
            new Brewery { Id = 4, Name = "Budweiser" },
            new Brewery { Id = 5, Name = "Coors Brewing Company" },
            new Brewery { Id = 6, Name = "Guinness" },
            new Brewery { Id = 7, Name = "Stella Artois" },
            new Brewery { Id = 8, Name = "Corona" },
            new Brewery { Id = 9, Name = "Pilsner Urquell" },
            new Brewery
            {
                Id = 10, Name = "SABMiller"
            } // Note: As of my last update, SABMiller was merged with AB InBev. You might want to use a brand like "Hoegaarden" or another specific beer brand from their portfolio.
        );
    }
}

