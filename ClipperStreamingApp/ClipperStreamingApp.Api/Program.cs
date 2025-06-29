using ClipperStreamingApp.Domain.Conta.Factory;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ClipperStreamingApp.Application.Interfaces;
using ClipperStreamingApp.Application.Services;
using ClipperStreamingApp.Domain.Conta.Repository;
using ClipperStreamingApp.Domain.Playlist.Repository;
using ClipperStreamingApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies; 

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IContaFactory, ContaFactory>();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<IPlaylistService, PlaylistService>();
builder.Services.AddScoped<IBandaRepository, BandaRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true; 
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("CSDbContext");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão 'CSDbContext' não foi encontrada.");
}

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


app.Run();
