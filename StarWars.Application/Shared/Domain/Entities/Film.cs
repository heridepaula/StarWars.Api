using System.Text.Json.Serialization;

namespace StarWars.Application.Shared.Domain.Entities
{
    public class Film
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateOnly ReleaseDate { get; set; }

        [JsonIgnore]
        public ICollection<Planet> Planets { get; set; }
    }
}
