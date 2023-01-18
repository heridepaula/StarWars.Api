using AutoFixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using StarWars.Application.Shared.Contexts;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Repositories;
using Xunit;

namespace StarWars.Api.UnitTests.Repositories
{
    public class StarWarsRepositoryTests : AbstractTests
    {
        private readonly ApplicationDbContext _context;

        public StarWarsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task StarWarsRepository_ShouldBeCreateAPlanet()
        {
            var sut = new StarWarsRepository(_context);

            var planet = CreatePlanetInstance();

            var result = await sut.CreatePlanetAsync(planet, default);

            Assert.True(result);
        }

        [Fact]
        public async Task StarWarsRepository_ShouldBeReturnAPlanetById()
        {
            var planet = CreatePlanetInstance();

            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            var sut = new StarWarsRepository(_context);

            var result = await sut.GetPlanetByIdAsync(planet.Id, default);

            Assert.NotNull(result);
            Assert.Equal(planet.Id, result.Id);
        }

        [Fact]
        public async Task StarWarsRepository_ShouldBeReturnAPlanetByName()
        {
            var planet = CreatePlanetInstance();

            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            var sut = new StarWarsRepository(_context);

            var result = await sut.GetPlanetByNameAsync(planet.Name, default);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task StarWarsRepository_ShouldBeReturnAFilmById()
        {
            var film = CreateFilmInstance();

            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            var sut = new StarWarsRepository(_context);

            var result = await sut.GetFilmById(film.Id, default);

            Assert.NotNull(result);
            Assert.Equal(film.Id, result.Id);
        }

        [Fact]
        public async Task StarWarsRepository_ShouldBeDeleteAAplanetByIds()
        {
            var planet = CreatePlanetInstance();

            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            var sut = new StarWarsRepository(_context);

            var result = await sut.DeletePlanetByIdAsync(planet, default);

            Assert.True(result);
        }

        [Fact]
        public async Task StarWarsRepository_ShouldBeFetchAllPlanets()
        {
            var planet = CreatePlanetInstance();

            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            var sut = new StarWarsRepository(_context);

            var result = await sut.FetchPlanetsAsync(default);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
