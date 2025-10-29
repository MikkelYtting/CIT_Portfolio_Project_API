namespace CIT_Portfolio_Project_API.Models.Entities;

public class UserRating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Tconst { get; set; } = default!;
    public int Value { get; set; }
}
