using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Interfaces;

namespace VideoGameCatalog.Api.Controllers;

/// <summary>Read-only — used to populate the Platform dropdown on the edit form.</summary>
[ApiController]
[Route("api/[controller]")]
public class PlatformsController(IPlatformService service) : ControllerBase
{
    // GET /api/platforms
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformDto>>> GetAll() =>
        Ok(await service.GetAllAsync());
}
