using Microsoft.AspNetCore.Mvc;
using Moq;
using VideoGameCatalog.Api.Controllers;
using VideoGameCatalog.Core.DTOs;
using VideoGameCatalog.Core.Interfaces;

namespace VideoGameCatalog.Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="GamesController"/>.
/// IVideoGameService is mocked with Moq so no database is required.
/// Every test follows the Arrange / Act / Assert (AAA) pattern.
/// </summary>
public class GamesControllerTests
{
    // ── Shared helpers ────────────────────────────────────────────────────────

    private static VideoGameDto MakeDto(int id = 1) => new(
        Id: id,
        Title: "Elden Ring",
        Developer: "FromSoftware",
        ReleaseYear: 2022,
        Description: "Open-world action RPG.",
        GenreId: 6,
        GenreName: "Action RPG",
        PlatformId: 3,
        PlatformName: "PC"
    );

    private static CreateUpdateVideoGameDto MakeCreateDto() => new(
        Title: "Elden Ring",
        Developer: "FromSoftware",
        ReleaseYear: 2022,
        Description: "Open-world action RPG.",
        GenreId: 6,
        PlatformId: 3
    );

    // ── GET /api/games ────────────────────────────────────────────────────────

    [Fact]
    public async Task GetAll_WhenGamesExist_ReturnsOkWithList()
    {
        // Arrange
        var games = new List<VideoGameDto> { MakeDto(1), MakeDto(2) };
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(games);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<IEnumerable<VideoGameDto>>(ok.Value);
        Assert.Equal(2, returned.Count());
    }

    [Fact]
    public async Task GetAll_WhenNoGamesExist_ReturnsOkWithEmptyList()
    {
        // Arrange
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync([]);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsAssignableFrom<IEnumerable<VideoGameDto>>(ok.Value);
        Assert.Empty(returned);
    }

    // ── GET /api/games/{id} ───────────────────────────────────────────────────

    [Fact]
    public async Task GetById_WhenGameExists_ReturnsOkWithDto()
    {
        // Arrange
        var expected = MakeDto(1);
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(expected);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.GetById(1);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<VideoGameDto>(ok.Value);
        Assert.Equal(expected.Id, returned.Id);
        Assert.Equal(expected.Title, returned.Title);
    }

    [Fact]
    public async Task GetById_WhenGameNotFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((VideoGameDto?)null);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.GetById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    // ── POST /api/games ───────────────────────────────────────────────────────

    [Fact]
    public async Task Create_WithValidDto_Returns201CreatedAtAction()
    {
        // Arrange
        var created = MakeDto(42);
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.CreateAsync(It.IsAny<CreateUpdateVideoGameDto>()))
                   .ReturnsAsync(created);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.Create(MakeCreateDto());

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(controller.GetById), createdAt.ActionName);
        Assert.Equal(42, ((VideoGameDto)createdAt.Value!).Id);
    }

    // ── PUT /api/games/{id} ───────────────────────────────────────────────────

    [Fact]
    public async Task Update_WhenGameExists_ReturnsOkWithUpdatedDto()
    {
        // Arrange
        var updated = MakeDto(1) with { Title = "Elden Ring: Shadow of the Erdtree" };
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.UpdateAsync(1, It.IsAny<CreateUpdateVideoGameDto>()))
                   .ReturnsAsync(updated);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.Update(1, MakeCreateDto());

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var returned = Assert.IsType<VideoGameDto>(ok.Value);
        Assert.Equal("Elden Ring: Shadow of the Erdtree", returned.Title);
    }

    [Fact]
    public async Task Update_WhenGameNotFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.UpdateAsync(99, It.IsAny<CreateUpdateVideoGameDto>()))
                   .ReturnsAsync((VideoGameDto?)null);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.Update(99, MakeCreateDto());

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    // ── DELETE /api/games/{id} ────────────────────────────────────────────────

    [Fact]
    public async Task Delete_WhenGameExists_Returns204NoContent()
    {
        // Arrange
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_WhenGameNotFound_Returns404()
    {
        // Arrange
        var mockService = new Mock<IVideoGameService>();
        mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);
        var controller = new GamesController(mockService.Object);

        // Act
        var result = await controller.Delete(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
