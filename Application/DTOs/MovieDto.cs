namespace CIT_Portfolio_Project_API.Application.DTOs;

public class MovieDto
{
    public string Tconst { get; set; } = default!;
    public string? Title { get; set; }
    public List<LinkDto> Links { get; set; } = new();
}
