namespace VideoGameCatalog.Core.Models;

public class Genre
{
    public int Id { get; set; }

    /// <summary>e.g. "Action-Adventure", "RPG", "Racing"</summary>
    public string Name { get; set; } = string.Empty;

    // Navigation property — one Genre has many VideoGames
    public ICollection<VideoGame> VideoGames { get; set; } = [];
}
