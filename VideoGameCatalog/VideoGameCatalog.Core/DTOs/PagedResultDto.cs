namespace VideoGameCatalog.Core.DTOs;

/// <summary>Generic paged response envelope returned by all paginated endpoints.</summary>
public record PagedResultDto<T>(
    IEnumerable<T> Items,
    int TotalCount,
    int Page,
    int PageSize
);
