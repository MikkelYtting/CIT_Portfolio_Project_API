using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class PersonProfessionTests
{
    [DataTestMethod]
    [DataRow("nm123", "actor")] // typical profession value
    [DataRow("nm999", typeof(string), 64)] // profession at max length (64)
    public void PersonProfession_Positive(string nconst, object profession, int len = -1)
    {
        string prof = profession is Type ? new string('a', len) : profession.ToString()!;
        var p = new PersonProfession { Nconst = nconst, Profession = prof };
        var results = ValidationHelper.Validate(p);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "actor")] // missing nconst
    [DataRow("nm12", "actor")] // nconst too short (< 3 digits)
    [DataRow("nm123", null)] // missing profession
    [DataRow("nm123", typeof(string), 65)] // profession length above max (64)
    public void PersonProfession_Negative(string? nconst, object? profession, int len = -1)
    {
        string? prof = profession as string;
        if (profession is Type) prof = new string('a', len);
        var p = new PersonProfession { Nconst = nconst!, Profession = prof! };
        var results = ValidationHelper.Validate(p);
        results.Should().NotBeEmpty();
    }
}
