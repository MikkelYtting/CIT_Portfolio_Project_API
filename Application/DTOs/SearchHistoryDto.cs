namespace CIT_Portfolio_Project_API.Application.DTOs;

public class SearchHistoryDto
{
    public string Text { get; set; } = default!;
    public DateTime SearchedAt { get; set; }
    public List<LinkDto> Links { get; set; } = new();
}
