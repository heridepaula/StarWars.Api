using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Application.Features.FetchPlanets.Models;
using StarWars.Application.Features.FetchPlanets.UseCase;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using Xunit;

namespace StarWars.Api.UnitTests.UseCases
{
    public class FetchPlanetsUseCaseTests : AbstractTests
    {
        [Fact]
        public async Task FetchPlanetsUseCase_ShouldBeReturnAListOfPlanets()
        {
            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.FetchPlanetsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateManyPlanetsInstance());

            var sut = Mocker.CreateInstance<FetchPlanetsUseCase>();

            var input = Fixture.Build<FetchPlanetsInput>().Create();
            
            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
            Assert.IsAssignableFrom<IEnumerable<Planet>>(result.Data);
        }

        [Fact]
        public async Task FetchPlanetsUseCase_ShouldBeThrowsException()
        {
            var exception = new Exception();

            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.FetchPlanetsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var sut = Mocker.CreateInstance<FetchPlanetsUseCase>();

            var input = Fixture.Build<FetchPlanetsInput>().Create();

            await Assert.ThrowsAsync<Exception>(() => sut.Handle(input, default));

            Mocker.GetMock<ILogger<FetchPlanetsUseCase>>().Verify(
            x => x.Log(
                It.Is<LogLevel>(l => l == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString() == $"[{nameof(FetchPlanetsUseCase)}] - {exception.Message}"),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}