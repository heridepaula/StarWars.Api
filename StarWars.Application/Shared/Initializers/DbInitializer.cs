using StarWars.Application.Shared.Contexts;

namespace StarWars.Application.Shared.Initializers
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
