using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RatingsController : ControllerBase
{
    private readonly IRatingManager _ratingManager;
    public RatingsController(IRatingManager ratingManager) { _ratingManager = ratingManager; }

    [HttpPost]
    public async Task<IActionResult> Rate([FromBody] RatingDto dto, CancellationToken ct)
    {
        // TODO: Extract userId from JWT claims
        if (!int.TryParse(User.Identity?.Name, out var userId))
        {
            // Fallback for now; in real code extract claim type "sub"
            var sub = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            userId = int.TryParse(sub, out var id) ? id : 0;
        }
        if (userId <= 0) return Unauthorized();
    await _ratingManager.RateAsync(userId, dto.Tconst, dto.Value, ct);
        return NoContent();
    }
}
