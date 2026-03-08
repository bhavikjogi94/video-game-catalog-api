using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Interfaces;
using VideoGameCatalog.Core.Models;
using VideoGameCatalog.Infrastructure.Data;

namespace VideoGameCatalog.Infrastructure.Services;

public class VideoGameService(AppDbContext context) : IVideoGameService
{
    // Reusable projection: Entity → DTO (keeps mapping in one place)
    private static VideoGameDto ToDto(VideoGame g) => new(
        g.Id, g.Title, g.Developer, g.ReleaseYear, g.Description,
        g.GenreId, g.Genre.Name, g.PlatformId, g.Platform.Name
    );

    public async Task<IEnumerable<VideoGameDto>> GetAllAsync() =>
        await context.VideoGames
            .Include(g => g.Genre)
            .Include(g => g.Platform)
            .OrderBy(g => g.Title)
            .Select(g => ToDto(g))
            .ToListAsync();

    public async Task<VideoGameDto?> GetByIdAsync(int id)
    {
        var game = await context.VideoGames
            .Include(g => g.Genre)
            .Include(g => g.Platform)
            .FirstOrDefaultAsync(g => g.Id == id);

        return game is null ? null : ToDto(game);
    }

    public async Task<VideoGameDto> CreateAsync(CreateUpdateVideoGameDto dto)
    {
        var game = new VideoGame
        {
            Title = dto.Title,
            Developer = dto.Developer,
            ReleaseYear = dto.ReleaseYear,
            Description = dto.Description,
            GenreId = dto.GenreId,
            PlatformId = dto.PlatformId
        };

        context.VideoGames.Add(game);
        await context.SaveChangesAsync();

        // Reload with navigation properties so the response is fully populated
        await context.Entry(game).Reference(g => g.Genre).LoadAsync();
        await context.Entry(game).Reference(g => g.Platform).LoadAsync();

        return ToDto(game);
    }

    public async Task<VideoGameDto?> UpdateAsync(int id, CreateUpdateVideoGameDto dto)
    {
        var game = await context.VideoGames.FindAsync(id);
        if (game is null) return null;

        game.Title = dto.Title;
        game.Developer = dto.Developer;
        game.ReleaseYear = dto.ReleaseYear;
        game.Description = dto.Description;
        game.GenreId = dto.GenreId;
        game.PlatformId = dto.PlatformId;

        await context.SaveChangesAsync();

        await context.Entry(game).Reference(g => g.Genre).LoadAsync();
        await context.Entry(game).Reference(g => g.Platform).LoadAsync();

        return ToDto(game);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await context.VideoGames.FindAsync(id);
        if (game is null) return false;

        context.VideoGames.Remove(game);
        await context.SaveChangesAsync();
        return true;
    }
}
