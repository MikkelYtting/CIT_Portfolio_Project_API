using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class PersonKnownForTests
{
    [DataTestMethod]
    [DataRow("nm123", "tt123")]
    public void PersonKnownFor_Positive(string nconst, string tconst)
    {
        var pk = new PersonKnownFor { Nconst = nconst, Tconst = tconst };
        var results = ValidationHelper.Validate(pk);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, "tt123")]
    [DataRow("nm12", "tt123")]
    [DataRow("nm123", null)]
    [DataRow("nm123", "tt12")]
    public void PersonKnownFor_Negative(string? nconst, string? tconst)
    {
        var pk = new PersonKnownFor { Nconst = nconst!, Tconst = tconst! };
        var results = ValidationHelper.Validate(pk);
        results.Should().NotBeEmpty();
    }
}
