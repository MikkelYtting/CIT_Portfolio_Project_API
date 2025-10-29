namespace CIT_Portfolio_Project_API.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public List<LinkDto> Links { get; set; } = new();
}
