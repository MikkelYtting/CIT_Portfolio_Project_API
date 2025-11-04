using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class WordIndexTests
{
    [DataTestMethod]
    [DataRow("word", 0)] // lower bound frequency 0 with typical word
    [DataRow(128, 100)] // word length at max (128)
    public void WordIndex_Positive(object arg1, int freq)
    {
        string w = arg1 is int len ? new string('a', len) : arg1.ToString()!;
        var wi = new WordIndex { Word = w, Frequency = freq };
        var results = ValidationHelper.Validate(wi);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow(null, 0)] // missing word
    [DataRow(129, 0)] // word length above max (128)
    [DataRow("a", -1)] // negative frequency
    public void WordIndex_Negative(object? arg1, int freq)
    {
        string? w = arg1 switch
        {
            null => null,
            int len => new string('a', len),
            _ => arg1.ToString()
        };
        var wi = new WordIndex { Word = w!, Frequency = freq };
        var results = ValidationHelper.Validate(wi);
        results.Should().NotBeEmpty();
    }
}
