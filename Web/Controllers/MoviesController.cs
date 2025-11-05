using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    // Exposes read-only movie resources (list, details, search) and delegates to the movie manager.
    private readonly IMovieManager _manager;
    public MoviesController(IMovieManager manager) { _manager = manager; }

    [HttpGet]
    // List movies with pagination (page defaults to 1, pageSize to 20).
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.GetMoviesAsync(page, pageSize, ct));

    [HttpGet("{tconst}")]
    // Get a single movie by its stable ID (tconst). 404 if not found.
    public async Task<IActionResult> GetById(string tconst, CancellationToken ct)
    {
        var dto = await _manager.GetByIdAsync(tconst, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("search")]
    // Free-text search with pagination.
    public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.SearchAsync(query, page, pageSize, ct));

    [HttpGet("structured")]
    // Structured search across optional fields (title/plot/characters/person).
    public async Task<IActionResult> Structured([FromQuery] string? title, [FromQuery] string? plot, [FromQuery] string? characters, [FromQuery] string? person, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.StructuredSearchAsync(title, plot, characters, person, page, pageSize, ct));
}
