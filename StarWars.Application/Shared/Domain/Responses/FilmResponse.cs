using System.Text.Json.Serialization;

namespace StarWars.Application.Shared.Domain.Responses
{
    public class FilmResponse
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("director")]
        public string Director { get; set; }

        [JsonPropertyName("release_date")]
        public DateOnly ReleaseDate { get; set; }
    }
}
