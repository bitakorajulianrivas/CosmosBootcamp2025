using FluentAssertions;

namespace KatasTDD.Tests.PasswordValidators;

public class PasswordValidator3UnitTest
{
    [Fact]
    public void IsValid_IfTheInputIsNull_ReturnFalse()
    {
        string input = null;

        bool isValid = new PasswordValidator3().IsValid(input);

        isValid.Should().BeFalse();
    }
}

public class PasswordValidator3
{
    public bool IsValid(string? input)
    {
        return input != null;
    }
}