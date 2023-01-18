using AutoFixture;
using Moq;
using StarWars.Application.Features.GetPlanetByName.Models;
using StarWars.Application.Features.GetPlanetByName.UseCase;
using StarWars.Application.Features.LoadPlanetById.Models;
using StarWars.Application.Features.LoadPlanetById.UseCase;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;
using Xunit;

namespace StarWars.Api.UnitTests.UseCases
{
    public class LoadPlanetByIdUseCaseTests : AbstractTests
    {
        [Fact]
        public async Task LoadPlanetByIdUseCase_ShouldBeLoadPlanetByRepository()
        {
            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreatePlanetInstance());

            var sut = Mocker.CreateInstance<LoadPlanetByIdUseCase>();

            var input = Fixture.Build<LoadPlanetByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<Planet>>(result);
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<Planet>(result.Data);
        }

        [Fact]
        public async Task LoadPlanetByIdUseCase_ShouldBeLogWarningWhenPlanetIdNotExistsInApi()
        {
            var sut = Mocker.CreateInstance<LoadPlanetByIdUseCase>();

            var input = Fixture.Build<LoadPlanetByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<Planet>>(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task LoadPlanetByIdUseCase_ShouldBeCreateAPlanetInDatabase()
        {
            Mocker.GetMock<IStarWarsApiService>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreatePlanetResponseInstance());

            var sut = Mocker.CreateInstance<LoadPlanetByIdUseCase>();

            var input = Fixture.Build<LoadPlanetByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Mocker.GetMock<IStarWarsRepository>().Verify(x => x.CreatePlanetAsync(It.IsAny<Planet>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task LoadPlanetByIdUseCase_ShouldBeThrowsException()
        {
            var exception = new Exception();

            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var sut = Mocker.CreateInstance<LoadPlanetByIdUseCase>();

            var input = Fixture.Build<LoadPlanetByIdInput>().Create();

            await Assert.ThrowsAsync<Exception>(() => sut.Handle(input, default));
        }
    }
}
