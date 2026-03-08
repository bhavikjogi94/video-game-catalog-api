using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Interfaces;
using VideoGameCatalog.Infrastructure.Data;

namespace VideoGameCatalog.Infrastructure.Services;

public class PlatformService(AppDbContext context) : IPlatformService
{
    public async Task<IEnumerable<PlatformDto>> GetAllAsync() =>
        await context.Platforms
            .OrderBy(p => p.Name)
            .Select(p => new PlatformDto(p.Id, p.Name, p.Manufacturer))
            .ToListAsync();
}
