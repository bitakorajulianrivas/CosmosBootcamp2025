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

    [Fact]
    public void IsValid_IfTheInputDoesNotContainANumber_ReturnFalse()
    {
        string input = "PassswordWithoutANumber";

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainAnUnderscore_ReturnFalse()
    {
        string input = "P4sssw0rdWithoutUnderscore";

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfInputHasAllRules_ReturnTrue()
    {
        string input = "P4sssW0rd_W1thOut_Und3rsc0r3";

        bool isValid = PasswordValidator.IsValid(input);

        isValid.Should().BeTrue();
    }
}

public static class PasswordValidator
{
    public static bool IsValid(string input)
    {
        return input.IsNotNullAndNotEmpty() &&
               input.IsMoreThanEightCharacters() &&
               input.ContainsCapitalLetters() &&
               input.ContainsLowerCaseLetters()  &&
               input.ContainsDigits() &&
               input.ContainsUnderscores();
    }

    private static bool IsNotNullAndNotEmpty(this string input) => !string.IsNullOrEmpty(input);
    private static bool IsMoreThanEightCharacters(this string input) => input.Length >= 8;
    private static bool ContainsCapitalLetters(this string input) => input.Any(char.IsUpper);
    private static bool ContainsLowerCaseLetters(this string input) => input.Any(char.IsLower);
    private static bool ContainsDigits(this string input) => input.Any(char.IsDigit);
    private static bool ContainsUnderscores(this string input) => input.Any(ch => ch == '_');
}