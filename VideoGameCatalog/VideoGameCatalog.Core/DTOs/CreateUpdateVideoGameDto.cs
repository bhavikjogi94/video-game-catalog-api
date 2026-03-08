using System.ComponentModel.DataAnnotations;

namespace VideoGameCatalog.Core.DTOs;

/// <summary>Payload accepted by POST /api/games and PUT /api/games/{id}.</summary>
public record CreateUpdateVideoGameDto(
    [Required, MaxLength(200)] string Title,
    [Required, MaxLength(150)] string Developer,
    [Range(1950, 2100)] int ReleaseYear,
    [MaxLength(1000)] string? Description,
    [Required] int GenreId,
    [Required] int PlatformId
);
