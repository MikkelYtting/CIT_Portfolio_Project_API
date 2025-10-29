using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
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
    public async Task<IActionResult> Get(int userId, CancellationToken ct)
    => Ok(await _manager.GetAsync(userId, ct));

    public record AddBookmarkRequest(string Tconst, string? Note);

    [HttpPost]
    public async Task<IActionResult> Add(int userId, [FromBody] AddBookmarkRequest request, CancellationToken ct)
    {
        // TODO: Enforce userId from JWT instead of route for security
    await _manager.AddAsync(userId, request.Tconst, request.Note, ct);
        return NoContent();
    }

    [HttpDelete("{tconst}")]
    public async Task<IActionResult> Delete(int userId, string tconst, CancellationToken ct)
    {
    await _manager.DeleteAsync(userId, tconst, ct);
        return NoContent();
    }
}
