using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MovieDetailTests
{
    [DataTestMethod]
    [DataRow("tt123", null, null, null)]
    [DataRow("tt999", "plot", 0, 1990)]
    public void MovieDetail_Positive(string tconst, string? plot, int? runtime, int? startYear)
    {
        var d = new MovieDetail { Tconst = tconst, Plot = plot, RuntimeMinutes = runtime, StartYear = startYear };
        var results = ValidationHelper.Validate(d);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, null, null, null)]
    [DataRow("tt12", null, -1, null)]
    [DataRow("nm123", null, -5, null)]
    public void MovieDetail_Negative(string? tconst, string? plot, int? runtime, int? startYear)
    {
        var d = new MovieDetail { Tconst = tconst!, Plot = plot, RuntimeMinutes = runtime, StartYear = startYear };
        var results = ValidationHelper.Validate(d);
        results.Should().NotBeEmpty();
    }
}
