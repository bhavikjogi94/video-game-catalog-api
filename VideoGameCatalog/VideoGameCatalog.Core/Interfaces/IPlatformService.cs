using VideoGameCatalog.Core.DTOs;

namespace VideoGameCatalog.Core.Interfaces;

public interface IPlatformService
{
    Task<IEnumerable<PlatformDto>> GetAllAsync();
}
