using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Interfaces;
using VideoGameCatalog.Infrastructure.Data;

namespace VideoGameCatalog.Infrastructure.Services;

public class GenreService(AppDbContext context) : IGenreService
{
    public async Task<IEnumerable<GenreDto>> GetAllAsync() =>
        await context.Genres
            .OrderBy(g => g.Name)
            .Select(g => new GenreDto(g.Id, g.Name))
            .ToListAsync();
}
