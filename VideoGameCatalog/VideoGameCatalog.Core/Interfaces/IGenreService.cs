using VideoGameCatalog.Core.DTOs;

namespace VideoGameCatalog.Core.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> GetAllAsync();
}
