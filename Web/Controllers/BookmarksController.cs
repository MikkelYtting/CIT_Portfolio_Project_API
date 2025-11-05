using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/users/{userId:int}/[controller]")]
[Authorize]
public class BookmarksController : ControllerBase
{
    private readonly IBookmarkManager _manager;
    public BookmarksController(IBookmarkManager manager) { _manager = manager; }

    [HttpGet]
    public async Task<IActionResult> Get(int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        var tokenUserId = User.GetUserId();
        if (tokenUserId is null || tokenUserId <= 0) return Unauthorized();
        if (tokenUserId.Value != userId) return Forbid();
        return Ok(await _manager.GetAsync(tokenUserId.Value, page, pageSize, ct));
    }

    public record AddBookmarkRequest(string Tconst, string? Note);

    [HttpPost]
    public async Task<IActionResult> Add(int userId, [FromBody] AddBookmarkRequest request, CancellationToken ct)
    {
        var tokenUserId = User.GetUserId();
        if (tokenUserId is null || tokenUserId <= 0) return Unauthorized();
        if (tokenUserId.Value != userId) return Forbid();
        await _manager.AddAsync(tokenUserId.Value, request.Tconst, request.Note, ct);
        return NoContent();
    }

    [HttpDelete("{tconst}")]
    public async Task<IActionResult> Delete(int userId, string tconst, CancellationToken ct)
    {
        var tokenUserId = User.GetUserId();
        if (tokenUserId is null || tokenUserId <= 0) return Unauthorized();
        if (tokenUserId.Value != userId) return Forbid();
        await _manager.DeleteAsync(tokenUserId.Value, tconst, ct);
        return NoContent();
    }
}
