using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieManager _manager;
    public MoviesController(IMovieManager manager) { _manager = manager; }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.GetMoviesAsync(page, pageSize, ct));

    [HttpGet("{tconst}")]
    public async Task<IActionResult> GetById(string tconst, CancellationToken ct)
    {
    var dto = await _manager.GetByIdAsync(tconst, ct);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.SearchAsync(query, page, pageSize, ct));

    [HttpGet("structured")]
    public async Task<IActionResult> Structured([FromQuery] string? title, [FromQuery] string? plot, [FromQuery] string? characters, [FromQuery] string? person, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.StructuredSearchAsync(title, plot, characters, person, page, pageSize, ct));
}
