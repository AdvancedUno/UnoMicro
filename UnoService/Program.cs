using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using UnoService.Data;
using UnoService.SyncDataService.Http;
using UnoService.SyncDataService.HttpContext;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
builder.Services.AddControllers();
builder.Services.AddScoped<IUnoRepo, UnoRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

var Configuration = builder.Configuration;
Console.WriteLine($"--> CommandService Endpoint {Configuration["CommandService"]}");


// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global error handling
app.UseExceptionHandler("/error");
app.Map("/error", (HttpContext context) =>
{
    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogError(exception, "Unhandled exception");
    return Results.Problem("An error occurred while processing your request.");
});

app.UseAuthorization();
app.MapControllers();

// Seed the database
PrepDb.PrepPopulation(app);

app.Run();