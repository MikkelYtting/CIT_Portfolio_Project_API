using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class RatingTests
{
    [DataTestMethod]
    [DataRow("tt123", 0.0, 0)]
    [DataRow("tt123", 10.0, 100)]
    [DataRow("tt999", 5.5, 1)]
    public void Rating_Positive(string tconst, double avg, int votes)
    {
        var r = new Rating { Tconst = tconst, AverageRating = avg, NumVotes = votes };
        var results = ValidationHelper.Validate(r);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, -0.1, -1)]
    [DataRow("tt12", 10.1, -5)] // bad tconst, avg out of range, negative votes
    [DataRow("nm123", -1.0, 0)] // wrong prefix
    public void Rating_Negative(string? tconst, double avg, int votes)
    {
        var r = new Rating { Tconst = tconst!, AverageRating = avg, NumVotes = votes };
        var results = ValidationHelper.Validate(r);
        results.Should().NotBeEmpty();
    }
}
