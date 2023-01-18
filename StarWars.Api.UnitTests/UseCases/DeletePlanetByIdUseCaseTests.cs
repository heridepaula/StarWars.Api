using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using StarWars.Application.Features.DeletePlanetById.Models;
using StarWars.Application.Features.DeletePlanetById.UseCase;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using Xunit;

namespace StarWars.Api.UnitTests.UseCases
{
    public class DeletePlanetByIdUseCaseTests : AbstractTests
    {
        [Fact]
        public async Task DeletePlanetByIdUseCase_ShouldBeDeleteAnExistingPlanet()
        {
            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreatePlanetInstance());

            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.DeletePlanetByIdAsync(It.IsAny<Planet>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var sut = Mocker.CreateInstance<DeletePlanetByIdUseCase>();

            var input = Fixture.Build<DeletePlanetByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.True(result);
        }

        [Fact]
        public async Task DeletePlanetByIdUseCase_ShouldBeLogWarningWhenPlanetNotExists()
        {
            var sut = Mocker.CreateInstance<DeletePlanetByIdUseCase>();

            var input = Fixture.Build<DeletePlanetByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Mocker.GetMock<ILogger<DeletePlanetByIdUseCase>>().Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Warning),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == $"[{nameof(DeletePlanetByIdUseCase)}] - No planets found for Id {input.Id}"),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

            Assert.False(result);
        }

        [Fact]
        public async Task DeletePlanetByIdUseCase_ShouldBeThrowsException()
        {
            var exception = new Exception();

            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetPlanetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            var sut = Mocker.CreateInstance<DeletePlanetByIdUseCase>();

            var input = Fixture.Build<DeletePlanetByIdInput>().Create();

            await Assert.ThrowsAsync<Exception>(() => sut.Handle(input, default));

            Mocker.GetMock<ILogger<DeletePlanetByIdUseCase>>().Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString() == $"[{nameof(DeletePlanetByIdUseCase)}] - {exception.Message}"),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));
        }
    }
}
