using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class MovieDetailTests
{
    [DataTestMethod]
    [DataRow("tt123", null, null, null)] // only required tconst
    [DataRow("tt999", "plot", 0, 1990)] // runtime at lower bound 0
    public void MovieDetail_Positive(string tconst, string? plot, int? runtime, int? startYear)
    {
        var d = new MovieDetail { Tconst = tconst, Plot = plot, RuntimeMinutes = runtime, StartYear = startYear };
        var results = ValidationHelper.Validate(d);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, null, null, null)] // missing tconst
    [DataRow("tt12", null, -1, null)] // tconst too short, runtime negative
    [DataRow("nm123", null, -5, null)] // wrong tconst prefix, runtime negative
    public void MovieDetail_Negative(string? tconst, string? plot, int? runtime, int? startYear)
    {
        var d = new MovieDetail { Tconst = tconst!, Plot = plot, RuntimeMinutes = runtime, StartYear = startYear };
        var results = ValidationHelper.Validate(d);
        results.Should().NotBeEmpty();
    }
}
