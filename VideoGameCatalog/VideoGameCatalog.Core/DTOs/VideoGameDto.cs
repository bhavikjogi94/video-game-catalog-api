namespace VideoGameCatalog.Core.DTOs;

/// <summary>What the API returns when listing or fetching a game.</summary>
public record VideoGameDto(
    int Id,
    string Title,
    string Developer,
    int ReleaseYear,
    string? Description,
    int GenreId,
    string GenreName,
    int PlatformId,
    string PlatformName
);
