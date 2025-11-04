using CIT_Portfolio_Project_API.Models.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CIT_Portfolio_Project_API.UnitTests.Models;

[TestClass]
public class UserTests
{
    [DataTestMethod]
    [DataRow("bob", "bob@example.com", 20)] // username >=3, email valid, min pass hash length
    [DataRow("a_very_long_username_but_valid", "a@b.co", 50)] // typical valid values
    [DataRow(64, "ok@example.com", 200)] // username at max 64 chars, pass hash at max 200
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
    [DataRow("", "", 10)] // empty username/email invalid
    [DataRow(65, "toolong@example.com", 10)] // username longer than 64
    [DataRow("valid", "invalid@", 200)] // invalid email format
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
