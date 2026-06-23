namespace PlanetariumApp.Models
{
    public class CelestialBody
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Mass { get; set; }
        public string? DistanceFromEarth { get; set; }
        public string? ImagePath { get; set; }

        // Зовнішній ключ для зв'язку з сузір'ям
        public int? ConstellationId { get; set; }
        public Constellation? Constellation { get; set; }
    }
}