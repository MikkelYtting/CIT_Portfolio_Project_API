using System.ComponentModel.DataAnnotations;
using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MovieTests
{
    [DataTestMethod]
    [DataRow("tt123")] // min regex: 'tt' + 3 digits
    [DataRow("tt1234567890")] // within length <= 20
    public void Tconst_Positive(string tconst)
    {
        var m = new Movie { Tconst = tconst, Title = "The Dark Knight" };
        var results = ValidationHelper.Validate(m);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null)] // required violation
    [DataRow("")] // empty not allowed
    [DataRow("nm123")] // wrong prefix (should be 'tt')
    [DataRow("tt12")] // too short: needs >= 3 digits
    [DataRow("tt12345678901234567890")] // length > 20
    public void Tconst_Negative(string? tconst)
    {
        var m = new Movie { Tconst = tconst!, Title = "x" };
        var results = ValidationHelper.Validate(m);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow("")] // empty title allowed
    [DataRow("123")] // numeric string allowed
    [DataRow("valid title")] // typical title
    [DataRow("x")] // 1 char title
    [DataRow(null)] // optional
    public void Title_Positive(string? value)
    {
        var m = new Movie { Tconst = "tt123", Title = value };
        var results = ValidationHelper.Validate(m);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(513)] // just above max 512
    [DataRow(514)] // further above max
    public void Title_Negative_Length_Too_Long(int length)
    {
        var title = new string('a', length);
        var m = new Movie { Tconst = "tt123", Title = title };
        var results = ValidationHelper.Validate(m);
        results.Should().ContainSingle(r => r.MemberNames.Contains("Title"));
    }
}
