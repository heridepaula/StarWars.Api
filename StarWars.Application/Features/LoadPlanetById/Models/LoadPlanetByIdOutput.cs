namespace StarWars.Application.Features.LoadPlanetById.Models
{
    public class LoadPlanetByIdOutput
    {
        public bool Success { get; set; }

        public LoadPlanetByIdOutput(bool success)
        {
            Success = success;
        }
    }
}
