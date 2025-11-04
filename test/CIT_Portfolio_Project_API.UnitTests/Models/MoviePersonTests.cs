using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MoviePersonTests
{
    [DataTestMethod]
    [DataRow("tt123", "nm123", null)] // optional category (null) allowed
    [DataRow("tt999", "nm999", "actor")] // typical category within length
    public void MoviePerson_Positive(string tconst, string nconst, string? category)
    {
        var mp = new MoviePerson { Tconst = tconst, Nconst = nconst, Category = category };
        var results = ValidationHelper.Validate(mp);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "nm123", null)] // missing tconst
    [DataRow("tt12", "nm123", null)] // tconst too short (< 3 digits)
    [DataRow("tt123", null, null)] // missing nconst
    [DataRow("tt123", "nm12", null)] // nconst too short (< 3 digits)
    public void MoviePerson_Negative(string? tconst, string? nconst, string? category)
    {
        var mp = new MoviePerson { Tconst = tconst!, Nconst = nconst!, Category = category };
        var results = ValidationHelper.Validate(mp);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(65)] // category length above max (64)
    public void MoviePerson_Negative_Category_Too_Long(int len)
    {
        var cat = new string('a', len);
        var mp = new MoviePerson { Tconst = "tt123", Nconst = "nm123", Category = cat };
        var results = ValidationHelper.Validate(mp);
        results.Should().NotBeEmpty();
    }
}
