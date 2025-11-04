using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserSearchHistoryTests
{
    [DataTestMethod]
    [DataRow(1, "a", "2024-01-01T00:00:00Z")] // userId lower bound with 1-char text
    [DataRow(10, "valid text", "2025-11-04T00:00:00Z")] // typical values
    public void UserSearchHistory_Positive(int userId, string text, string date)
    {
        var dt = DateTime.Parse(date);
        var sh = new UserSearchHistory { UserId = userId, Text = text, SearchedAt = dt };
        var results = ValidationHelper.Validate(sh);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(0, "a", "2024-01-01T00:00:00Z")] // userId below 1
    [DataRow(-1, "a", "2024-01-01T00:00:00Z")] // negative userId
    [DataRow(1, null, "2024-01-01T00:00:00Z")] // missing text
    public void UserSearchHistory_Negative(int userId, string? text, string date)
    {
        var dt = DateTime.Parse(date);
        var sh = new UserSearchHistory { UserId = userId, Text = text!, SearchedAt = dt };
        var results = ValidationHelper.Validate(sh);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(513)] // text length above max (512)
    public void UserSearchHistory_Negative_Text_Too_Long(int len)
    {
        var text = new string('a', len);
        var sh = new UserSearchHistory { UserId = 1, Text = text, SearchedAt = DateTime.UtcNow };
        var results = ValidationHelper.Validate(sh);
        results.Should().NotBeEmpty();
    }
}
