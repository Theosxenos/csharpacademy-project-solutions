using ExerciseTracker.Controllers;
using ExerciseTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ExerciseContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<MainController>();

var host = builder.Build();

host.Services.GetRequiredService<MainController>().ShowMenu();

host.Run();

