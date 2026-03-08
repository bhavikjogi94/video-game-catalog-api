namespace VideoGameCatalog.Core.Models;

public class Platform
{
    public int Id { get; set; }

    /// <summary>e.g. "PlayStation 5", "Nintendo Switch", "PC"</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>e.g. "Sony", "Nintendo", "Multiple"</summary>
    public string Manufacturer { get; set; } = string.Empty;

    // Navigation property — one Platform has many VideoGames
    public ICollection<VideoGame> VideoGames { get; set; } = [];
}
