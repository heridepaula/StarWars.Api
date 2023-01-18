using Microsoft.EntityFrameworkCore;
using StarWars.Application.Shared.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace StarWars.Application.Shared.Contexts
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Film> Films { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planet>()
                .HasMany(x => x.Films)
                .WithMany(x => x.Planets)
                .UsingEntity(x => x.ToTable("PlanetsFims"))
                .ToTable("Planets");

            modelBuilder.Entity<Film>()
                .HasMany(x => x.Planets)
                .WithMany(x => x.Films)
                .UsingEntity(x => x.ToTable("PlanetsFims"))
                .ToTable("Films");
        }
    }
}
