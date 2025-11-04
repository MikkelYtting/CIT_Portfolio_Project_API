using System.ComponentModel.DataAnnotations;

namespace CIT_Portfolio_Project_API.Models.Entities;

public class WordIndex
{
    [Required]
    [StringLength(128)]
    public string Word { get; set; } = default!;

    [Range(0, int.MaxValue)]
    public int Frequency { get; set; }
}
