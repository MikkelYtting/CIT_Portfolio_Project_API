using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MovieGenreTests
{
    [DataTestMethod]
    [DataRow("tt123", "action")]
    [DataRow("tt999", "drama")]
    public void MovieGenre_Positive(string tconst, string genre)
    {
        var mg = new MovieGenre { Tconst = tconst, Genre = genre };
        var results = ValidationHelper.Validate(mg);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "action")]
    [DataRow("tt12", "action")]
    [DataRow("nm123", "action")]
    [DataRow("tt123", null)]
    public void MovieGenre_Negative(string? tconst, string? genre)
    {
        var mg = new MovieGenre { Tconst = tconst!, Genre = genre! };
        var results = ValidationHelper.Validate(mg);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(65)]
    public void MovieGenre_Negative_Genre_Too_Long(int len)
    {
        var g = new string('a', len);
        var mg = new MovieGenre { Tconst = "tt123", Genre = g };
        var results = ValidationHelper.Validate(mg);
        results.Should().NotBeEmpty();
    }
}
