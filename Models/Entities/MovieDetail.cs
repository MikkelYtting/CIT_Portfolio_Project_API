using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CIT_Portfolio_Project_API.Models.Entities;

public class MovieDetail
{
    [Required]
    [StringLength(20)]
    [RegularExpression("^tt\\d{3,}$", ErrorMessage = "Tconst must look like 'tt123' (IMDB key)")]
    public string Tconst { get; set; } = default!;
    public string? Plot { get; set; }
    [Range(0, int.MaxValue)]
    public int? RuntimeMinutes { get; set; }
    public int? StartYear { get; set; }
}
