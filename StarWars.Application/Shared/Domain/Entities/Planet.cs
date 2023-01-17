namespace StarWars.Application.Shared.Domain.Entities
{
    public class Planet
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public ICollection<Film> Films { get;set; }
    }
}
