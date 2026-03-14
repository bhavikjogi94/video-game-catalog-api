using Microsoft.AspNetCore.Mvc;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Exceptions;
using VideoGameCatalog.Core.Interfaces;

namespace VideoGameCatalog.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController(IVideoGameService service) : ControllerBase
{
    // GET /api/games?page=1&pageSize=10&search=&sortBy=title&sortDir=asc
    [HttpGet]
    public async Task<ActionResult<PagedResultDto<VideoGameDto>>> GetAll([FromQuery] GameQueryParams query) =>
        Ok(await service.GetAllAsync(query));


    // GET /api/games/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<VideoGameDto>> GetById(int id)
    {
        var game = await service.GetByIdAsync(id);
        if (game is null) throw new NotFoundException($"Video game with ID {id} was not found.");
        return Ok(game);
    }

    // POST /api/games
    [HttpPost]
    public async Task<ActionResult<VideoGameDto>> Create(CreateUpdateVideoGameDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/games/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult<VideoGameDto>> Update(int id, CreateUpdateVideoGameDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        if (updated is null) throw new NotFoundException($"Video game with ID {id} was not found.");
        return Ok(updated);
    }

    // DELETE /api/games/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted) throw new NotFoundException($"Video game with ID {id} was not found.");
        return NoContent();
    }
}
