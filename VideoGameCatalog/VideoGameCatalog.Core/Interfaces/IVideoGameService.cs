using VideoGameCatalog.Core.DTOs;

namespace VideoGameCatalog.Core.Interfaces;

public interface IVideoGameService
{
    Task<IEnumerable<VideoGameDto>> GetAllAsync();
    Task<VideoGameDto?> GetByIdAsync(int id);
    Task<VideoGameDto> CreateAsync(CreateUpdateVideoGameDto dto);
    Task<VideoGameDto?> UpdateAsync(int id, CreateUpdateVideoGameDto dto);
    Task<bool> DeleteAsync(int id);
}
