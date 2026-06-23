using System.Collections.Generic;

namespace PlanetariumApp.Models
{
    public class Constellation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? LatinName { get; set; }
        public string? Symbolism { get; set; }
        public string? BestWatchingTime { get; set; }
        public string? ImagePath { get; set; }

        // одне сузір'я може мати багато космічних об'єктів
        public List<CelestialBody> CelestialBodies { get; set; } = new();
    }
}