namespace PlanetariumApp.Models
{
    public class CosmicMission
    {
        public int Id { get; set; }
        public string DiscoveryName { get; set; } = string.Empty;
        public string MissionOrShuttle { get; set; } = string.Empty;
        public int DiscoveryYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
    }
}