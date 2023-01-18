using StarWars.Application.Shared.Contexts;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Application.Shared.Initializers
{
    [ExcludeFromCodeCoverage]
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
