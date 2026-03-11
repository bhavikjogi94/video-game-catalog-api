using System.ComponentModel.DataAnnotations;

namespace VideoGameCatalog.Core.DTOs;

/// <summary>Query-string parameters accepted by GET /api/games for server-side paging, filtering, and sorting.</summary>
public class GameQueryParams
{
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 50)]
    public int PageSize { get; set; } = 10;

    /// <summary>Case-insensitive partial match on Title.</summary>
    public string? Search { get; set; }

    /// <summary>Column to sort by: title | developer | releaseYear | genre | platform</summary>
    public string? SortBy { get; set; }

    /// <summary>Sort direction: asc | desc (defaults to asc)</summary>
    public string? SortDir { get; set; }
}
