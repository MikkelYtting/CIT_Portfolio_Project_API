using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserRatingTests
{
    [DataTestMethod]
    [DataRow(1, "tt123", 1)] // userId min valid, tconst valid, value lower bound
    [DataRow(10, "tt999", 10)] // value upper bound
    [DataRow(2, "tt123", 5)] // typical middle value
    public void UserRating_Positive(int userId, string tconst, int value)
    {
        var ur = new UserRating { UserId = userId, Tconst = tconst, Value = value };
        var results = ValidationHelper.Validate(ur);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(0, "tt123", 1)] // userId below 1
    [DataRow(-1, "tt123", 10)] // negative userId
    [DataRow(1, null, 5)] // missing tconst
    [DataRow(1, "nm123", 5)] // wrong tconst prefix (should be 'tt')
    [DataRow(1, "tt12", 5)] // tconst too short
    [DataRow(1, "tt123", 0)] // rating value below min (1)
    [DataRow(1, "tt123", 11)] // rating value above max (10)
    public void UserRating_Negative(int userId, string? tconst, int value)
    {
        var ur = new UserRating { UserId = userId, Tconst = tconst!, Value = value };
        var results = ValidationHelper.Validate(ur);
        results.Should().NotBeEmpty();
    }
}
