namespace VideoGameCatalog.Core.Models;

public class VideoGame
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Developer { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string? Description { get; set; }

    // Foreign key — VideoGame belongs to one Genre
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;

    // Foreign key — VideoGame belongs to one Platform
    public int PlatformId { get; set; }
    public Platform Platform { get; set; } = null!;
}
