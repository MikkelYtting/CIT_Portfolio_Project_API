namespace CIT_Portfolio_Project_API.Application.DTOs;

public class BookmarkDto
{
    public string Tconst { get; set; } = default!;
    public string? Title { get; set; }
    public string? Note { get; set; }
    public List<LinkDto> Links { get; set; } = new();
}
