using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SearchController : ControllerBase
{
    // Exposes search endpoints (free-text and structured). Delegates to the search manager.
    private readonly ISearchManager _manager;
    public SearchController(ISearchManager manager) { _manager = manager; }

    [HttpGet]
<<<<<<< HEAD
    // Free-text search with pagination. userId may be used for user-specific logging/analytics.
    public async Task<IActionResult> StringSearch([FromQuery] int userId, [FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.StringSearchAsync(userId, query, page, pageSize, ct));

    [HttpGet("structured")]
    // Structured search across optional fields (title/plot/characters/person).
    public async Task<IActionResult> Structured([FromQuery] int userId, [FromQuery] string? title, [FromQuery] string? plot, [FromQuery] string? characters, [FromQuery] string? person, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    => Ok(await _manager.StructuredSearchAsync(userId, title, plot, characters, person, page, pageSize, ct));
=======
    public async Task<IActionResult> StringSearch([FromQuery, DefaultValue("dark knight")] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var userId = User.GetUserId();
        if (userId is null || userId <= 0) return Unauthorized();
        return Ok(await _manager.StringSearchAsync(userId.Value, query, page, pageSize, ct));
    }

    [HttpGet("structured")]
    public async Task<IActionResult> Structured([FromQuery, DefaultValue("dark knight")] string? title, [FromQuery, DefaultValue("")] string? plot, [FromQuery, DefaultValue("")] string? characters, [FromQuery, DefaultValue("Christian Bale")] string? person, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var userId = User.GetUserId();
        if (userId is null || userId <= 0) return Unauthorized();
        return Ok(await _manager.StructuredSearchAsync(userId.Value, title, plot, characters, person, page, pageSize, ct));
    }
>>>>>>> upstream/main
}
