using AutoFixture;
using Moq;
using StarWars.Application.Features.GetPlanetByName.Models;
using StarWars.Application.Features.GetPlanetByName.UseCase;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;
using Xunit;

namespace StarWars.Api.UnitTests.UseCases
{
    public class GetPlanetByNameUseCaseTests : AbstractTests
    {
        [Fact]
        public async Task GetPlanetByNameUseCase_ShouldBeGetAListOfPlanetsByName()
        {
            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateManyPlanetsInstance());

            var sut = Mocker.CreateInstance<GetPlanetByNameUseCase>();

            var input = Fixture.Build<GetPlanetByNameInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<IEnumerable<Planet>>>(result);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
            Assert.IsAssignableFrom<IEnumerable<Planet>>(result.Data);
        }

        [Fact]
        public async Task GetPlanetByNameUseCase_ShouldBeThrowsException()
        {
            var exception = new Exception();

            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var sut = Mocker.CreateInstance<GetPlanetByNameUseCase>();

            var input = Fixture.Build<GetPlanetByNameInput>().Create();

            await Assert.ThrowsAsync<Exception>(() => sut.Handle(input, default));
        }
    }
}
