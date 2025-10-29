namespace CIT_Portfolio_Project_API.Models.Entities;

public class MoviePerson
{
    public string Tconst { get; set; } = default!;
    public string Nconst { get; set; } = default!;
    public string? Category { get; set; } // actor, director, writer, etc.
}
