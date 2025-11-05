using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    // Exposes search endpoints (free-text and structured). Delegates to the search manager.
    private readonly ISearchManager _manager;
    public SearchController(ISearchManager manager) { _manager = manager; }

    [HttpGet]
    // Free-text search with pagination. userId may be used for user-specific logging/analytics.
    public async Task<IActionResult> StringSearch([FromQuery] int userId, [FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.StringSearchAsync(userId, query, page, pageSize, ct));

    [HttpGet("structured")]
    // Structured search across optional fields (title/plot/characters/person).
    public async Task<IActionResult> Structured([FromQuery] int userId, [FromQuery] string? title, [FromQuery] string? plot, [FromQuery] string? characters, [FromQuery] string? person, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.StructuredSearchAsync(userId, title, plot, characters, person, page, pageSize, ct));
}
