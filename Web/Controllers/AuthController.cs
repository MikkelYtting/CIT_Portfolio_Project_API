using CIT_Portfolio_Project_API.Application.DTOs.Auth;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _auth;
    public AuthController(IAuthManager auth) { _auth = auth; }

    /// <summary>
    /// Login with username and password to get an authentication token
    /// </summary>
    /// <param name="username">Your username</param>
    /// <param name="password">Your password</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>JWT token and user information on success</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromQuery][Required] string username,
        [FromQuery][Required] string password,
        CancellationToken ct)
    {
            var res = await _auth.LoginAsync(new LoginRequest { Username = username, Password = password }, ct);
        return res is null ? Unauthorized() : Ok(res);
    }
}
