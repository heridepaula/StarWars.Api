using AutoFixture;
using Moq;
using Moq.Protected;
using StarWars.Application.Shared.Domain.Responses;
using StarWars.Application.Shared.Services;
using System.Net;
using Xunit;

namespace StarWars.Api.UnitTests.Services
{
    public class StarWarsApiServiceTests : AbstractTests
    {
        private const string StarWarsPlanetsApiResponseJson = "{\"name\":\"name\",\"climate\":\"climate\",\"terrain\":\"terrain\"}";
        private const string StarWarsFilmsApiResponseJson = "{\"title\":\"title\",\"director\":\"director\",\"release_date\":\"0001-01-01\"}";

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task StarWarsApiService_ShouldBeGetAPlanetById(long id)
        {
            Mocker.Use(CreateHttpClientFactoryMock(StarWarsPlanetsApiResponseJson, HttpStatusCode.OK));

            var sut = Mocker.CreateInstance<StarWarsApiService>();
            var result = await sut.GetPlanetByIdAsync(id, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<PlanetResponse>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task StarWarsApiService_ShouldBeReturnNullPlanetWhenStatusCodeIsNotOk(long id)
        {
            Mocker.Use(CreateHttpClientFactoryMock(StarWarsPlanetsApiResponseJson, HttpStatusCode.NotFound));

            var sut = Mocker.CreateInstance<StarWarsApiService>();
            var result = await sut.GetPlanetByIdAsync(id, default);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task StarWarsApiService_ShouldBeGetAFilmById(long id)
        {
            Mocker.Use(CreateHttpClientFactoryMock(StarWarsFilmsApiResponseJson, HttpStatusCode.OK));

            var sut = Mocker.CreateInstance<StarWarsApiService>();
            var result = await sut.GetFilmByIdAsync(id, default);

            Assert.NotNull(result);
            Assert.IsAssignableFrom<FilmResponse>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task StarWarsApiService_ShouldBeReturnNullFilmWhenStatusCodeIsNotOk(long id)
        {
            Mocker.Use(CreateHttpClientFactoryMock(StarWarsFilmsApiResponseJson, HttpStatusCode.NotFound));

            var sut = Mocker.CreateInstance<StarWarsApiService>();
            var result = await sut.GetFilmByIdAsync(id, default);

            Assert.Null(result);
        }

        private Mock<IHttpClientFactory> CreateHttpClientFactoryMock(string json, HttpStatusCode statusCode)
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(json),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = Fixture.Create<Uri>();
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            return httpClientFactory;
        }
    }
}
