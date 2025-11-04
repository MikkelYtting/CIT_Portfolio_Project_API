using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MovieGenreTests
{
    [DataTestMethod]
    [DataRow("tt123", "action")] // typical valid tconst + genre
    [DataRow("tt999", "drama")] // another valid genre value
    public void MovieGenre_Positive(string tconst, string genre)
    {
        var mg = new MovieGenre { Tconst = tconst, Genre = genre };
        var results = ValidationHelper.Validate(mg);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "action")] // missing tconst
    [DataRow("tt12", "action")] // tconst too short (< 3 digits)
    [DataRow("nm123", "action")] // wrong tconst prefix (should be 'tt')
    [DataRow("tt123", null)] // missing genre
    public void MovieGenre_Negative(string? tconst, string? genre)
    {
        var mg = new MovieGenre { Tconst = tconst!, Genre = genre! };
        var results = ValidationHelper.Validate(mg);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(65)] // genre length above max (64)
    public void MovieGenre_Negative_Genre_Too_Long(int len)
    {
        var g = new string('a', len);
        var mg = new MovieGenre { Tconst = "tt123", Genre = g };
        var results = ValidationHelper.Validate(mg);
        results.Should().NotBeEmpty();
    }
}
