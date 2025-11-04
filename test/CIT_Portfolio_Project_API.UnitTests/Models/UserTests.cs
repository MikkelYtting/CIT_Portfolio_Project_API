using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserTests
{
    [DataTestMethod]
    [DataRow("bob", "bob@example.com", 20)] // min pass hash length
    [DataRow("a_very_long_username_but_valid", "a@b.co", 50)]
    [DataRow(64, "ok@example.com", 200)] // max username via length, max passwordhash
    public void User_Positive(object usernameArg, string email, int passLen)
    {
        var uname = usernameArg is int len ? new string('a', len) : usernameArg.ToString()!;
        var u = new User
        {
            Username = uname,
            Email = email,
            PasswordHash = new string('x', passLen)
        };
        var results = ValidationHelper.Validate(u);
        results.Should().BeEmpty();
    }

    [DataTestMethod]
    [DataRow("ab", "bad-email", 19)] // username too short, bad email, pass too short
    [DataRow("", "", 10)] // empty values
    [DataRow(65, "toolong@example.com", 10)] // username too long
    [DataRow("valid", "invalid@", 200)] // invalid email
    public void User_Negative(object usernameArg, string email, int passLen)
    {
        var uname = usernameArg is int len ? new string('a', len) : usernameArg.ToString()!;
        var u = new User
        {
            Username = uname,
            Email = email,
            PasswordHash = new string('x', passLen)
        };
        var results = ValidationHelper.Validate(u);
        results.Should().NotBeEmpty();
    }
}
