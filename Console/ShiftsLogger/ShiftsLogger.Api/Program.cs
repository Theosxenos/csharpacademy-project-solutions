using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Api.Data;
using ShiftsLogger.Api.DTOs.Request;
using ShiftsLogger.Api.Repositories;
using ShiftsLogger.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShiftsLoggerContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SecretConnection")));

builder.Services.AddScoped<Repository>();
builder.Services.AddScoped<WorkerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var apiGroup = app.MapGroup("/api/");
var workerGroup = apiGroup.MapGroup("worker");
workerGroup.MapGet("/", () => "Yo");
workerGroup.MapPost("/", async (WorkerRequest worker, WorkerService workerService) =>
{
    try
    {
        var result = await workerService.AddWorker(worker);
        return Results.Created((string?)null, result);
    }
    catch (ArgumentNullException e)
    {
        return Results.Problem(e.Message, title: "Request can not be null.", statusCode: 400);
    }
    catch (ArgumentException e)
    {
        return Results.Problem(e.Message, statusCode: 400);
    }

});


app.Run();