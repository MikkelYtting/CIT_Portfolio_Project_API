namespace CIT_Portfolio_Project_API.Models.Entities;

public class MovieDetail
{
    public string Tconst { get; set; } = default!;
    public string? Plot { get; set; }
    public int? RuntimeMinutes { get; set; }
    public int? StartYear { get; set; }
}
