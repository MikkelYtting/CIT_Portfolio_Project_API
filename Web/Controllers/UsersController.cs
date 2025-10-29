using CIT_Portfolio_Project_API.Application.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CIT_Portfolio_Project_API.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserManager _manager;
    public UsersController(IUserManager manager) { _manager = manager; }

    public record RegisterRequest(string Username, string Email, string Password);

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
    var dto = await _manager.RegisterAsync(request.Username, request.Email, request.Password, ct);
        return Created($"/api/users/{dto.Id}", dto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
    var dto = await _manager.GetByIdAsync(id, ct);
        return dto is null ? NotFound() : Ok(dto);
    }
}
