using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CIT_Portfolio_Project_API.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class RatingTests
{
    // Factory
    private Rating CreateRating(string? tconst, double avg, int votes) => new Rating
    {
        Tconst = tconst!,
        AverageRating = avg,
        NumVotes = votes
    };

    private bool TryValidateModel(object model, out ICollection<ValidationResult> results)
    {
        var ctx = new ValidationContext(model);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(model, ctx, results, validateAllProperties: true);
    }

    // Tconst only
    [DataTestMethod]
    [DataRow("tt123")]
    [DataRow("tt000")]
    [DataRow("tt123456789012345678")] // <= 20
    public void Rating_Tconst_ShouldPass(string tconst)
    {
        // Arrange
        var model = CreateRating(tconst, avg: 5.0, votes: 1);
        // Act
        var ok = TryValidateModel(model, out var results);
        // Assert
        Assert.IsTrue(ok);
        Assert.AreEqual(0, results.Count);
    }

    [DataTestMethod]
    [DataRow(null)]
    [DataRow("")]
    [DataRow("tt12")] // too short
    [DataRow("nm123")] // wrong prefix
    [DataRow("tt123456789012345678901")] // > 20
    public void Rating_Tconst_ShouldFail(string? tconst)
    {
        // Arrange
        var model = CreateRating(tconst, avg: 5.0, votes: 1);
        // Act
        var ok = TryValidateModel(model, out var results);
        // Assert
        Assert.IsFalse(ok);
        Assert.IsTrue(results.Count > 0);
    }

    // AverageRating only
    [DataTestMethod]
    [DataRow(0.0)]
    [DataRow(1.0)]
    [DataRow(5.5)]
    [DataRow(10.0)]
    public void Rating_AverageRating_ShouldPass(double avg)
    {
        // Arrange
        var model = CreateRating("tt123", avg, votes: 1);
        // Act
        var ok = TryValidateModel(model, out var results);
        // Assert
        Assert.IsTrue(ok);
        Assert.AreEqual(0, results.Count);
    }

    [DataTestMethod]
    [DataRow(-0.1)]
    [DataRow(10.1)]
    public void Rating_AverageRating_ShouldFail(double avg)
    {
        // Arrange
        var model = CreateRating("tt123", avg, votes: 1);
        // Act
        var ok = TryValidateModel(model, out var results);
        // Assert
        Assert.IsFalse(ok);
        Assert.IsTrue(results.Count > 0);
    }

    // NumVotes only
    [DataTestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(42)]
    [DataRow(int.MaxValue - 2)]
    [DataRow(int.MaxValue - 1)]
    [DataRow(int.MaxValue)]
    public void Rating_NumVotes_ShouldPass(int votes)
    {
        // Arrange
        var model = CreateRating("tt123", avg: 5.0, votes: votes);
        // Act
        var ok = TryValidateModel(model, out var results);
        // Assert
        Assert.IsTrue(ok);
        Assert.AreEqual(0, results.Count);
    }

    [DataTestMethod]
    [DataRow(-1)]
    [DataRow(-5)]
    public void Rating_NumVotes_ShouldFail(int votes)
    {
        // Arrange
        var model = CreateRating("tt123", avg: 5.0, votes: votes);
        // Act
        var ok = TryValidateModel(model, out var results);
        // Assert
        Assert.IsFalse(ok);
        Assert.IsTrue(results.Count > 0);
    }
}
