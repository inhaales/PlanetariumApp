namespace PlanetariumApp.Models
{
    public class Glossary
    {
        public int Id { get; set; }
        public string Term { get; set; } = string.Empty;
        public string Definition { get; set; } = string.Empty;
        public string? Category { get; set; }
    }
}