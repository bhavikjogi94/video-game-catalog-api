using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Interfaces;

namespace VideoGameCatalog.Api.Controllers;

/// <summary>Read-only — used to populate the Genre dropdown on the edit form.</summary>
[ApiController]
[Route("api/[controller]")]
public class GenresController(IGenreService service) : ControllerBase
{
    // GET /api/genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll() =>
        Ok(await service.GetAllAsync());
}
