﻿using ExerciseTracker.Controllers;
using ExerciseTracker.Data;
using ExerciseTracker.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ExerciseContext>(opt => 
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ExerciseDapperContext>();

builder.Services.AddTransient<MainController>();
builder.Services.AddScoped<SquatsController>();

builder.Services.AddScoped<SquatsRepository>();

var host = builder.Build();

var context = host.Services.GetRequiredService<ExerciseContext>();
context.Database.Migrate();

while (true)
{
    await host.Services.GetRequiredService<MainController>().ShowMenu();
}

// host.Run();


