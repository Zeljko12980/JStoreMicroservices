using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Dodaj CORS
// CORS konfiguracija
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // frontend React dev server
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Dodaj ovo AKO koristiš cookies ili auth
    });
});


// Rate limiter konfiguracija
builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

var app = builder.Build();

// Use CORS pre ReverseProxy
app.UseCors("AllowFrontend");

// Rate limiter
app.UseRateLimiter();

// Reverse Proxy
app.MapReverseProxy();

app.Run();
