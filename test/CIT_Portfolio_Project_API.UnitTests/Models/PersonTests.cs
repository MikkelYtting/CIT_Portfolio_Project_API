using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class PersonTests
{
    [DataTestMethod]
    [DataRow("nm123")] // min regex: 'nm' + 3 digits
    [DataRow("nm123456")] // within length <= 20
    public void Nconst_Positive(string nconst)
    {
        var p = new Person { Nconst = nconst, Name = "Christian Bale" };
        var results = ValidationHelper.Validate(p);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null)] // required violation
    [DataRow("")] // empty not allowed
    [DataRow("tt123")] // wrong prefix (should be 'nm')
    [DataRow("nm12")]  // too short < 3 digits
    [DataRow("nm12345678901234567890")] // length > 20
    public void Nconst_Negative(string? nconst)
    {
        var p = new Person { Nconst = nconst!, Name = "x" };
        var results = ValidationHelper.Validate(p);
        results.Should().NotBeEmpty();
    }

    [DataTestMethod]
    [DataRow("")] // empty allowed
    [DataRow("A")] // 1 char
    [DataRow(null)] // optional
    public void Name_Positive(string? value)
    {
        var p = new Person { Nconst = "nm123", Name = value };
        var results = ValidationHelper.Validate(p);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(257)] // just above max 256
    [DataRow(300)] // further above max
    public void Name_Negative_Too_Long(int length)
    {
        var name = new string('a', length);
        var p = new Person { Nconst = "nm123", Name = name };
        var results = ValidationHelper.Validate(p);
        results.Should().NotBeEmpty();
    }
}
