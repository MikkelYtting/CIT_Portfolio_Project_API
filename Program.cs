using System.Text;
using AutoMapper;
using CIT_Portfolio_Project_API.Infrastructure.Mapping;
using CIT_Portfolio_Project_API.Infrastructure.Persistence;
using CIT_Portfolio_Project_API.Infrastructure.Security;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Repositories.Implementations;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Application.Managers.Implementations;
using CIT_Portfolio_Project_API.Web.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers(options =>
{
	options.Filters.Add<PagingValidationFilter>();
});

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load .env (optional). If .env is missing, Env.Load() is a no-op and we'll fall back to configuration.
Env.Load();

// Prefer a concrete ConnectionStrings:Default if provided via configuration (e.g., tests inject it),
// otherwise try environment variables (POSTGRES_*), and finally fall back to appsettings.*
string connectionString;
string sourceLabel = "appsettings.json";

var configuredConn = builder.Configuration.GetConnectionString("Default");
bool looksPlaceholder = string.IsNullOrWhiteSpace(configuredConn)
	|| configuredConn.Contains("YOUR_DB", StringComparison.OrdinalIgnoreCase)
	|| configuredConn.Contains("YOUR_DEV_DB", StringComparison.OrdinalIgnoreCase)
	|| configuredConn.Contains("YOUR_USER", StringComparison.OrdinalIgnoreCase)
	|| configuredConn.Contains("YOUR_PASSWORD", StringComparison.OrdinalIgnoreCase);

if (!looksPlaceholder)
{
	connectionString = configuredConn!;
	sourceLabel = "configuration";
}
else
{
	// Build connection string from environment variables; if missing, fall back to appsettings.json
	string? host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
	string? port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
	string? db   = Environment.GetEnvironmentVariable("POSTGRES_DB");
	string? user = Environment.GetEnvironmentVariable("POSTGRES_USER");
	string? pw   = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

	if (!string.IsNullOrWhiteSpace(host) && !string.IsNullOrWhiteSpace(port)
		&& !string.IsNullOrWhiteSpace(db) && !string.IsNullOrWhiteSpace(user)
		&& !string.IsNullOrWhiteSpace(pw))
	{
		connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pw}";
		sourceLabel = ".env";
	}
	else
	{
		connectionString = builder.Configuration.GetConnectionString("Default")
							?? throw new InvalidOperationException("Connection string 'Default' not found.");
		sourceLabel = builder.Environment.IsDevelopment() ? "appsettings.Development.json" : "appsettings.json";
	}
}

// EF Core - Npgsql
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(connectionString)
		   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

// AutoMapper
builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ApiMappingProfile>(); // TODO: replace with actual validators when added

// JWT Auth
var jwtSection = builder.Configuration.GetSection("Jwt");
var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"] ?? "CHANGE_ME_SUPER_SECRET_KEY"));

builder.Services
	.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateIssuerSigningKey = true,
			ValidateLifetime = true,
			ValidIssuer = jwtSection["Issuer"],
			ValidAudience = jwtSection["Audience"],
			IssuerSigningKey = signingKey,
			ClockSkew = TimeSpan.FromMinutes(1)
		};
	});

builder.Services.AddAuthorization();

// Security helpers
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddSingleton<PasswordHasher>();

// DI: Repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();

// DI: Managers (previously Services)
builder.Services.AddScoped<IMovieManager, MovieManager>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<IBookmarkManager, BookmarkManager>();
builder.Services.AddScoped<IRatingManager, RatingManager>();
builder.Services.AddScoped<ISearchManager, SearchManager>();
builder.Services.AddScoped<IAnalyticsManager, AnalyticsManager>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	// Show detailed exceptions in Development
	app.UseDeveloperExceptionPage();

	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "CIT Portfolio API v1");
		// Serve Swagger UI at application root ('/')
		c.RoutePrefix = string.Empty;
	});
}
else
{
	// Generic error handler in non-Development
	app.UseExceptionHandler();
}

app.UseHttpsRedirection();
app.UseStatusCodePages();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Optional: If you prefer Swagger UI under /swagger instead of root,
// comment the RoutePrefix setting above and uncomment this redirect:
// app.MapGet("/", () => Results.Redirect("/swagger"));

// Log which connection source is being used (without secrets)
// Log which connection source is being used (without secrets)
try
{
	var csb = new Npgsql.NpgsqlConnectionStringBuilder(connectionString);
	Console.WriteLine($"DB config source: {sourceLabel} | Host={csb.Host} Port={csb.Port} Database={csb.Database} User={(string.IsNullOrEmpty(csb.Username) ? "(unset)" : csb.Username)}");
}
catch
{
	Console.WriteLine($"DB config source: {sourceLabel}");
}

// Optional DB connectivity probe endpoint (development aid)
app.MapGet("/debug/db-ping", async (AppDbContext dbCtx) =>
{
	try
	{
		var can = await dbCtx.Database.CanConnectAsync();
		return can ? Results.Ok("DB connection OK") : Results.Problem("Cannot connect to DB");
	}
	catch (Exception ex)
	{
		return Results.Problem($"DB connection failed: {ex.Message}");
	}
}).WithTags("Debug");

app.Run();

// Expose Program for WebApplicationFactory in integration tests
public partial class Program { }
