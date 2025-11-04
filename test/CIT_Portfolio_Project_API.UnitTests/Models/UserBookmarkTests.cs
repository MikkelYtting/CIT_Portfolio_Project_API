using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserBookmarkTests
{
    [DataTestMethod]
    [DataRow(1, "tt123", "note")]
    [DataRow(10, "tt999999", null)]
    public void Bookmark_Positive(int userId, string tconst, string? note)
    {
        var b = new UserBookmark { UserId = userId, Tconst = tconst, Note = note };
        var results = ValidationHelper.Validate(b);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(0, "tt123", "x")] // invalid user id
    [DataRow(-1, "tt123", "x")] // invalid user id
    [DataRow(1, null, "x")] // missing tconst
    [DataRow(1, "nm123", "x")] // wrong prefix
    [DataRow(1, "tt12", "x")] // too short
    public void Bookmark_Negative(int userId, string? tconst, string? note)
    {
        var b = new UserBookmark { UserId = userId, Tconst = tconst!, Note = note };
        var results = ValidationHelper.Validate(b);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow(513)]
    public void Bookmark_Negative_Note_Too_Long(int len)
    {
        var note = new string('a', len);
        var b = new UserBookmark { UserId = 1, Tconst = "tt123", Note = note };
        var results = ValidationHelper.Validate(b);
        results.Should().NotBeEmpty();
    }
}
