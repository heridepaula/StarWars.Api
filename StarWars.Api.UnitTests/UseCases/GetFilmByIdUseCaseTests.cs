using AutoFixture;
using Moq;
using StarWars.Application.Features.GetFilmById.Models;
using StarWars.Application.Features.GetFilmById.UseCase;
using StarWars.Application.Shared.Domain.Entities;
using StarWars.Application.Shared.Domain.Interfaces;
using StarWars.Application.Shared.Domain.Models;
using StarWars.Application.Shared.Domain.Responses;
using Xunit;

namespace StarWars.Api.UnitTests.UseCases
{
    public class GetFilmByIdUseCaseTests : AbstractTests
    {
        [Fact]
        public async Task GetFilmByIdUseCase_ShouldBeReturnAFilmByRepository()
        {
            Mocker.GetMock<IStarWarsRepository>()
                .Setup(x => x.GetFilmById(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateFilmInstance());

            var sut = Mocker.CreateInstance<GetFilmByIdUseCase>();

            var input = Fixture.Build<GetFilmByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<Film>>(result);
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<Film>(result.Data);
        }

        [Fact]
        public async Task GetFilmByIdUseCase_ShouldBeLogWarningWhenFilmNotExistsInApi()
        {
            var sut = Mocker.CreateInstance<GetFilmByIdUseCase>();

            var input = Fixture.Build<GetFilmByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<Film>>(result);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetFilmByIdUseCase_ShouldBeReturnAFilmByApi()
        {
            Mocker.GetMock<IStarWarsApiService>()
                .Setup(x => x.GetFilmByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CreateFilmResponseInstance());

            var sut = Mocker.CreateInstance<GetFilmByIdUseCase>();

            var input = Fixture.Build<GetFilmByIdInput>().Create();

            var result = await sut.Handle(input, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Output<Film>>(result);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetFilmByIdUseCase_ShouldBeThrowsException()
        {
            var exception = new Exception();

            Mocker.GetMock<IStarWarsRepository>()
            .Setup(x => x.GetFilmById(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(exception);

            var sut = Mocker.CreateInstance<GetFilmByIdUseCase>();

            var input = Fixture.Build<GetFilmByIdInput>().Create();

            await Assert.ThrowsAsync<Exception>(() => sut.Handle(input, default));
        }
    }
}
