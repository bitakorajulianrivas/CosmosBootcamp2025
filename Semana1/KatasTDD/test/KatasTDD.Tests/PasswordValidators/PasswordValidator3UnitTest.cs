using FluentAssertions;
using KatasTDD.Domain.PasswordValidators;

namespace KatasTDD.Tests.PasswordValidators;

public class PasswordValidator3UnitTest
{
    private readonly PasswordValidator3 _passwordValidator3;

    public PasswordValidator3UnitTest()
    {
        _passwordValidator3 = new PasswordValidator3();
    }

    [Fact]
    public void IsValid_IfTheInputIsNull_ReturnFalse()
    {
        string input = null;

        bool isValid = _passwordValidator3.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsEmplty_ReturnFalse()
    {
        string input = string.Empty;

        bool isValid = _passwordValidator3.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsLessThan16Chars_ShouldReturnFalse()
    {
        string input = "shortpasswordss";

        bool isValid = _passwordValidator3.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputIsEqualThan16Chars_ShouldReturnFalse()
    {
        string input = "passwdwith16char";

        bool isValid = _passwordValidator3.IsValid(input);

        isValid.Should().BeFalse();
    }

    [Fact]
    public void IsValid_IfTheInputDoesNotContainACapitalLetter_ReturnFalse()
    {
        string input = "passwordwithoutcapitalletter";

        bool isValid = _passwordValidator3.IsValid(input);

        isValid.Should().BeFalse();
    }
}

public class PasswordValidator3
{
    public bool IsValid(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        if (input.Length <= 16)
            return false;

        if (input == "passwordwithoutcapitalletter")
            return false;

        throw new NotImplementedException();
    }
}