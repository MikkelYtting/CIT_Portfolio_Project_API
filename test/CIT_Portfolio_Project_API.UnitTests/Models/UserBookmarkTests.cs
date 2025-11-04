using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserBookmarkTests
{
    [DataTestMethod]
    [DataRow(1, "tt123", "note")] // valid user, valid tconst, note within 512
    [DataRow(10, "tt999999", null)] // valid user, null note allowed
    public void Bookmark_Positive(int userId, string tconst, string? note)
    {
        var b = new UserBookmark { UserId = userId, Tconst = tconst, Note = note };
        var results = ValidationHelper.Validate(b);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(0, "tt123", "x")] // userId below 1
    [DataRow(-1, "tt123", "x")] // userId negative
    [DataRow(1, null, "x")] // missing tconst
    [DataRow(1, "nm123", "x")] // wrong tconst prefix
    [DataRow(1, "tt12", "x")] // tconst too short
    public void Bookmark_Negative(int userId, string? tconst, string? note)
    {
        var b = new UserBookmark { UserId = userId, Tconst = tconst!, Note = note };
        var results = ValidationHelper.Validate(b);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(513)] // note length > 512
    public void Bookmark_Negative_Note_Too_Long(int len)
    {
        var note = new string('a', len);
        var b = new UserBookmark { UserId = 1, Tconst = "tt123", Note = note };
        var results = ValidationHelper.Validate(b);
        results.Should().NotBeEmpty();
    }
}
