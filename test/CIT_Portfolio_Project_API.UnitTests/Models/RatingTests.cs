using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class RatingTests
{
    [DataTestMethod]
    [DataRow("tt123", 0.0, 0)] // lower bounds: avg=0, votes=0
    [DataRow("tt123", 10.0, 100)] // upper avg bound: 10
    [DataRow("tt999", 5.5, 1)] // typical values
    public void Rating_Positive(string tconst, double avg, int votes)
    {
        var r = new Rating { Tconst = tconst, AverageRating = avg, NumVotes = votes };
        var results = ValidationHelper.Validate(r);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, -0.1, -1)] // missing tconst, avg<0, votes<0
    [DataRow("tt12", 10.1, -5)] // tconst too short, avg>10, votes<0
    [DataRow("nm123", -1.0, 0)] // wrong prefix, avg<0
    public void Rating_Negative(string? tconst, double avg, int votes)
    {
        var r = new Rating { Tconst = tconst!, AverageRating = avg, NumVotes = votes };
        var results = ValidationHelper.Validate(r);
        results.Should().NotBeEmpty();
    }
}
