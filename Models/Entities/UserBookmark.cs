namespace CIT_Portfolio_Project_API.Models.Entities;

public class UserBookmark
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Tconst { get; set; } = default!;
    public string? Note { get; set; }
}
