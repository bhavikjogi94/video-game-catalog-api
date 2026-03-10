using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Core.Interfaces;
using VideoGameCatalog.Infrastructure.Data;
using VideoGameCatalog.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// ── EF Core ──────────────────────────────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Application Services (Dependency Injection) ───────────────────────────────
builder.Services.AddScoped<IVideoGameService, VideoGameService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IPlatformService, PlatformService>();

// ── CORS — allow Angular dev server ──────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAngularDev");
app.UseAuthorization();
app.MapControllers();

app.Run();

// Expose Program class for integration test factory (xUnit WebApplicationFactory)
public partial class Program { }
