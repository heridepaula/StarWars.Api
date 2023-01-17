using StarWars.Application.Features.DeletePlanetById.DependencyInjection;
using StarWars.Application.Features.GetFilmById.DependencyInjection;
using StarWars.Application.Features.GetPlanetByName.DependencyInjection;
using StarWars.Application.Features.LoadPlanetById.DependencyInjection;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.DependencyInjection;
using StarWars.Application.Shared.Initializers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(x =>
                    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDataBaseExtensions();
builder.Services.AddLoadPlanetByIdExtensions();
builder.Services.AddGetFilmByIdExtensions();
builder.Services.AddGetPlanetByNameExtensions();
builder.Services.AddDeletePlanetByIdExtensions();
builder.Services.AddServicesExtensions(builder.Configuration);

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
