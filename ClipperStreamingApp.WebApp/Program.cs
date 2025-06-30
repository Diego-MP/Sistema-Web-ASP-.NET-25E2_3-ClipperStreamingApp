using ClipperStreamingApp.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
        options.SlidingExpiration = true; 
        options.Cookie.HttpOnly = true; 
        options.Cookie.IsEssential = true;
    });



string apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:BaseUrl")
                    ?? throw new InvalidOperationException("A URL base da API 'ApiSettings:BaseUrl' não foi encontrada no appsettings.json.");

Action<HttpClient> configureApiClient = client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
};

builder.Services.AddHttpClient<IAuthService, AuthService>(configureApiClient);
builder.Services.AddHttpClient<IAssinaturaService, AssinaturaService>(configureApiClient);
builder.Services.AddHttpClient<IPlaylistService, PlaylistService>(configureApiClient);
builder.Services.AddHttpClient<ISearchService, SearchService>(configureApiClient);


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
       .WithStaticAssets();


app.Run();