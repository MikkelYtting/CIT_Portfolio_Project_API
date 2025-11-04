using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Security;
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
        var userId = User.GetUserId();
        if (userId is null || userId <= 0) return Unauthorized();
        await _ratingManager.RateAsync(userId.Value, dto.Tconst, dto.Value, ct);
        return NoContent();
    }
}
