using StarWars.Application.Features.GetFilmById.DependencyInjection;
using StarWars.Application.Features.LoadPlanetById.DependencyInjection;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.DependencyInjection;
using StarWars.Application.Shared.Initializers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataBaseExtensions();
builder.Services.AddLoadPlanetByIdExtensions();
builder.Services.AddGetFilmByIdExtensions();
builder.Services.AddServicesExtensions(builder.Configuration);

#region Migrations
using var scope = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
DbInitializer.Initialize(scope);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
