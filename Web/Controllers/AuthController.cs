using CIT_Portfolio_Project_API.Application.DTOs.Auth;
using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _auth;
    public AuthController(IAuthManager auth) { _auth = auth; }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var res = await _auth.LoginAsync(request, ct);
        return res is null ? Unauthorized() : Ok(res);
    }
}
