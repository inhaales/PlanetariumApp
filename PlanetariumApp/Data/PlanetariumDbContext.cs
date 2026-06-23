using Microsoft.EntityFrameworkCore;
using PlanetariumApp.Models;

namespace PlanetariumApp
{
    public class PlanetariumDbContext : DbContext
    {
        public DbSet<Glossary> Glossary { get; set; } = null!;
        public DbSet<Constellation> Constellations { get; set; } = null!;
        public DbSet<CelestialBody> CelestialBodies { get; set; } = null!;
        public DbSet<QuizQuestion> QuizQuestions { get; set; } = null!;
        public DbSet<CosmicMission> CosmicMissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.\\SQLEXPRESS;Database=PlanetariumDB;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}