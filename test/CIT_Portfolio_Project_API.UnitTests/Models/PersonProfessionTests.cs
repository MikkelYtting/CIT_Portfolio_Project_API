using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class PersonProfessionTests
{
    [DataTestMethod]
    [DataRow("nm123", "actor")]
    [DataRow("nm999", typeof(string), 64)]
    public void PersonProfession_Positive(string nconst, object profession, int len = -1)
    {
        string prof = profession is Type ? new string('a', len) : profession.ToString()!;
        var p = new PersonProfession { Nconst = nconst, Profession = prof };
        var results = ValidationHelper.Validate(p);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "actor")]
    [DataRow("nm12", "actor")]
    [DataRow("nm123", null)]
    [DataRow("nm123", typeof(string), 65)]
    public void PersonProfession_Negative(string? nconst, object? profession, int len = -1)
    {
        string? prof = profession as string;
        if (profession is Type) prof = new string('a', len);
        var p = new PersonProfession { Nconst = nconst!, Profession = prof! };
        var results = ValidationHelper.Validate(p);
        results.Should().NotBeEmpty();
    }
}
