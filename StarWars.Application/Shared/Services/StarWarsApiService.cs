using StarWars.Application.Shared.Domain.Responses;
using StarWars.Application.Shared.Domain.Interfaces;
using System.Text.Json;

namespace StarWars.Application.Shared.Services
{
    public class StarWarsApiService : IStarWarsApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StarWarsApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PlanetResponse> GetPlanetByIdAsync(long id, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient("StarWarsApi");

            var request = new HttpRequestMessage(HttpMethod.Get, $"planets/{id}");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync<PlanetResponse>(result, cancellationToken: cancellationToken);
        }

        public async Task<FilmResponse> GetFilmByIdAsync(long id, CancellationToken cancellationToken)
        {
            using var client = _httpClientFactory.CreateClient("StarWarsApi");

            var request = new HttpRequestMessage(HttpMethod.Get, $"films/{id}");

            var response = await client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadAsStreamAsync(cancellationToken);

            return await JsonSerializer.DeserializeAsync<FilmResponse>(result, cancellationToken: cancellationToken);
        }
    }
}
