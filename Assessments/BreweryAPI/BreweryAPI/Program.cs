var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<ValidateModelAttribute>();
        options.Filters.Add<ExceptionFilter>();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<BreweryDbContext>((options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DebianConnection"))
);
builder.Services.AddDbContext<BreweryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DebianConnection"))
);

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();

builder.Services.AddScoped<IPlaceOrderService, PlaceOrderService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWholesalerInventoryService, WholesalerInventoryService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BreweryDbContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
