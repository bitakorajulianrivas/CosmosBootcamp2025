using FluentAssertions;
using KatasTDD.Domain.PasswordValidators;

namespace KatasTDD.Tests.PasswordValidators;

public class PasswordValidator1UnitTest
{
    private readonly PasswordValidator _passwordValidator1;

    public PasswordValidator1UnitTest()
    {
        _passwordValidator1 = new PasswordValidator1();
    }

    [Fact]
    public void IsValid_IfTheInputIsNull_ReturnFalse()
    {
        string? input = null;

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsEmplty_ReturnFalse()
    {
        string input = string.Empty;

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsLessOrEqualToEightCaracters_ReturnFalse()
    {
        string input = "Password";

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainACapitalLetter_ReturnFalse()
    {
        string input = "passwordwithoutcapitalletter";

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainsALowercaseLetter_returnFalse()
    {
        string input = "PASSWORDWITHOUTLOWERCASELETTER";

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainANumber_ReturnFalse()
    {
        string input = "PassswordWithoutANumber";

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainAnUnderscore_ReturnFalse()
    {
        string input = "P4sssw0rdWithoutUnderscore";

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfInputHasAllRules_ReturnTrue()
    {
        string input = "P4sssW0rd_W1thOut_Und3rsc0r3";

        bool isValid = _passwordValidator1.IsValid(input);

        isValid.Should().BeTrue();
    }
}