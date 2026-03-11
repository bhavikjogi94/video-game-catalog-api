using VideoGameCatalog.Core.DTOs;

namespace VideoGameCatalog.Core.Interfaces;

public interface IVideoGameService
{
    Task<PagedResultDto<VideoGameDto>> GetAllAsync(GameQueryParams query);
    Task<VideoGameDto?> GetByIdAsync(int id);
    Task<VideoGameDto> CreateAsync(CreateUpdateVideoGameDto dto);
    Task<VideoGameDto?> UpdateAsync(int id, CreateUpdateVideoGameDto dto);
    Task<bool> DeleteAsync(int id);
}

