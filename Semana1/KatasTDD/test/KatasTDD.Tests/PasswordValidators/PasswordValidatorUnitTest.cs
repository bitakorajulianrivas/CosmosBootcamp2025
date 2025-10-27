using FluentAssertions;

namespace KatasTDD.Tests.PasswordValidators;

public class PasswordValidatorUnitTest
{
    [Fact]
    public void IsValid_IfTheInputIsNull_ReturnFalse()
    {
        string? input = null;

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsEmplty_ReturnFalse()
    {
        string? input = string.Empty;

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsLessOrEqualToEightCaracters_ReturnFalse()
    {
        string input = "Password";

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainACapitalLetter_ReturnFalse()
    {
        string input = "passwordwithoutcapitalletter";

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainsALowercaseLetter_returnFalse()
    {
        string input = "PASSWORDWITHOUTLOWERCASELETTER";

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }
}

public static class PasswordValidator
{
    public static bool IsValid(string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        if (input.Length <= 8)
        {
            return false;
        }

        if (input.Any(char.IsUpper))
        {
            return false;
        }

        throw new NotImplementedException();
    }
}