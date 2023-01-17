using System.Text.Json.Serialization;

namespace StarWars.Application.Shared.Domain.Responses
{
    public class PlanetResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("climate")]
        public string Climate { get; set; }

        [JsonPropertyName("terrain")]
        public string Terrain { get; set; }

        [JsonPropertyName("films")]
        public ICollection<string> Films { get; set; }
    }
}
