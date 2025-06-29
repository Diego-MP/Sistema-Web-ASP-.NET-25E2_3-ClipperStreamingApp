using DotNetEnv;
using ClipperStreamingApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("CSDbContext");

var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPass = Environment.GetEnvironmentVariable("DB_PASS");
var dbUrl = Environment.GetEnvironmentVariable("DB_URL");

string finalConnectionString = connectionString
    .Replace("{USER}", dbUser)
    .Replace("{PASS}", dbPass)
    .Replace("{URL}", dbUrl);

Console.WriteLine($"Connecting to {finalConnectionString}");

builder.Services.AddDbContext<StreamingDbContext>(options =>
    options.UseSqlServer(finalConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}