using Serilog;
using StarWars.Application.Features.DeletePlanetById.DependencyInjection;
using StarWars.Application.Features.GetFilmById.DependencyInjection;
using StarWars.Application.Features.GetPlanetByName.DependencyInjection;
using StarWars.Application.Features.LoadPlanetById.DependencyInjection;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.DependencyInjection;
using StarWars.Application.Shared.Initializers;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File($"./logs/StarWarsApi.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Logging.ClearProviders();
builder.Services.AddLogging(loggingBuilder =>
          loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataBaseExtensions(builder.Configuration);
builder.Services.AddLoadPlanetByIdExtensions();
builder.Services.AddGetFilmByIdExtensions();
builder.Services.AddGetPlanetByNameExtensions();
builder.Services.AddDeletePlanetByIdExtensions();
builder.Services.AddServicesExtensions(builder.Configuration);

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "StarWars.Api", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    opt.IncludeXmlComments(xmlPath);
});

#region Migrations
using var scope = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
DbInitializer.Initialize(scope);
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
