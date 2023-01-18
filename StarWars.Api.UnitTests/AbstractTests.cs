using AutoFixture;
using Moq.AutoMock;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Responses;

namespace StarWars.Api.UnitTests
{
    public abstract class AbstractTests
    {
        public Fixture Fixture { get; }
        public AutoMocker Mocker { get; }

        public AbstractTests()
        {
            Fixture = new Fixture();
            Mocker = new AutoMocker();
        }

        public Planet CreatePlanetInstance()
        {
            var films = Fixture.Build<Film>().Without(x => x.Planets).Without(x => x.ReleaseDate).CreateMany().ToList();
            return Fixture.Build<Planet>().With(x => x.Films, films).Create();
        }

        public ICollection<Planet> CreateManyPlanetsInstance()
        {
            var films = Fixture.Build<Film>().Without(x => x.Planets).Without(x => x.ReleaseDate).CreateMany().ToList();
            return Fixture.Build<Planet>().With(x => x.Films, films).CreateMany().ToList();
        }

        public Film CreateFilmInstance()
        {
            var planets = Fixture.Build<Planet>().Without(x => x.Films).CreateMany().ToList();
            return Fixture.Build<Film>().With(x => x.Planets, planets).Without(x => x.ReleaseDate).Create();
        }

        public FilmResponse CreateFilmResponseInstance()
        {
            return Fixture.Build<FilmResponse>().Without(x => x.ReleaseDate).Create();
        }

        public PlanetResponse CreatePlanetResponseInstance()
        {
            var films = new List<string>
            {
                "https://swapi.dev/api/films/1/",
                "https://swapi.dev/api/films/2/"
            };

            return Fixture.Build<PlanetResponse>().With(x => x.Films, films).Create();
        }
    }
}
