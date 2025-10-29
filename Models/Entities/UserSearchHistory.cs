namespace CIT_Portfolio_Project_API.Models.Entities;

public class UserSearchHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = default!;
    public DateTime SearchedAt { get; set; }
}
