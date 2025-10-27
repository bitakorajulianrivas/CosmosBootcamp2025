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
}

public static class PasswordValidator
{
    public static bool IsValid(string? input)
    {
        return false;
    }
}