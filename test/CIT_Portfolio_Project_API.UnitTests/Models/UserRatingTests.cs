using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserRatingTests
{
    [DataTestMethod]
    [DataRow(1, "tt123", 1)]
    [DataRow(10, "tt999", 10)]
    [DataRow(2, "tt123", 5)]
    public void UserRating_Positive(int userId, string tconst, int value)
    {
        var ur = new UserRating { UserId = userId, Tconst = tconst, Value = value };
        var results = ValidationHelper.Validate(ur);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(0, "tt123", 1)]
    [DataRow(-1, "tt123", 10)]
    [DataRow(1, null, 5)]
    [DataRow(1, "nm123", 5)]
    [DataRow(1, "tt12", 5)]
    [DataRow(1, "tt123", 0)]
    [DataRow(1, "tt123", 11)]
    public void UserRating_Negative(int userId, string? tconst, int value)
    {
        var ur = new UserRating { UserId = userId, Tconst = tconst!, Value = value };
        var results = ValidationHelper.Validate(ur);
        results.Should().NotBeEmpty();
    }
}
