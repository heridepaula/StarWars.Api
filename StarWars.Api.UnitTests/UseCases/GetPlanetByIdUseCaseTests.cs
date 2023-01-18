using AutoFixture;
using Moq;
using StarWars.Application.Features.GetPlanetById.Models;
using StarWars.Application.Features.GetPlanetById.UseCase;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;
using Xunit;

namespace StarWars.Api.UnitTests.UseCases
{
    public class GetPlanetByIdUseCaseTests : AbstractTests
    {
        [Fact]
        public async Task GetPlanetByIdUseCase_ShouldBeReturnAnPlanetById()
        {
            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreatePlanetInstance());

            var sut = Mocker.CreateInstance<GetPlanetByIdUseCase>();

            var input = Fixture.Build<GetPlanetByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<Planet>>(result);
        }

        [Fact]
        public async Task GetPlanetByIdUseCase_ShouldBeThrowsException()
        {
            var exception = new Exception();

            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var sut = Mocker.CreateInstance<GetPlanetByIdUseCase>();

            var input = Fixture.Build<GetPlanetByIdInput>().Create();

            await Assert.ThrowsAsync<Exception>(() => sut.Handle(input, default));
        }
    }
}
