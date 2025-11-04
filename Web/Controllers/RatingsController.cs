using CIT_Portfolio_Project_API.Application.DTOs;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using CIT_Portfolio_Project_API.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RatingsController : ControllerBase
{
    private readonly IRatingManager _ratingManager;
    public RatingsController(IRatingManager ratingManager) { _ratingManager = ratingManager; }

    /// <summary>
    /// Rate en film med din bedømmelse (1-10)
    /// </summary>
    /// <param name="tconst">IMDB ID for filmen (starter med 'tt')</param>
    /// <param name="value">Din bedømmelse (1-10)</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Ingen data returneres ved succes</returns>
    [HttpPost]
    public async Task<IActionResult> Rate(
        [FromQuery][Required][RegularExpression(@"^tt\d+$", ErrorMessage = "Tconst skal starte med 'tt' efterfulgt af tal")] string tconst,
        [FromQuery][Required][Range(1, 10, ErrorMessage = "Rating skal være mellem 1 og 10")] int value,
        CancellationToken ct)
    {
        var userId = User.GetUserId();
        if (userId is null || userId <= 0) return Unauthorized();
        await _ratingManager.RateAsync(userId.Value, tconst, value, ct);
        return NoContent();
    }
}
