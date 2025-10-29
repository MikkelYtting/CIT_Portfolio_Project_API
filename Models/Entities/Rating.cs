namespace CIT_Portfolio_Project_API.Models.Entities;

public class Rating
{
    public string Tconst { get; set; } = default!;
    public double AverageRating { get; set; }
    public int NumVotes { get; set; }
}
