using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MoviePersonTests
{
    [DataTestMethod]
    [DataRow("tt123", "nm123", null)]
    [DataRow("tt999", "nm999", "actor")]
    public void MoviePerson_Positive(string tconst, string nconst, string? category)
    {
        var mp = new MoviePerson { Tconst = tconst, Nconst = nconst, Category = category };
        var results = ValidationHelper.Validate(mp);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "nm123", null)]
    [DataRow("tt12", "nm123", null)]
    [DataRow("tt123", null, null)]
    [DataRow("tt123", "nm12", null)]
    public void MoviePerson_Negative(string? tconst, string? nconst, string? category)
    {
        var mp = new MoviePerson { Tconst = tconst!, Nconst = nconst!, Category = category };
        var results = ValidationHelper.Validate(mp);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(65)]
    public void MoviePerson_Negative_Category_Too_Long(int len)
    {
        var cat = new string('a', len);
        var mp = new MoviePerson { Tconst = "tt123", Nconst = "nm123", Category = cat };
        var results = ValidationHelper.Validate(mp);
        results.Should().NotBeEmpty();
    }
}
