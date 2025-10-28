using FluentAssertions;

namespace KatasTDD.Tests.PasswordValidators
{
    public class PasswordValitator2UnitTest
    {
        [Fact]
        public void IsValid_IfTheInputIsNull_ReturnFalse()
        {
            string input = null;

            bool isValid = PasswordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }
    }

    public class PasswordValidator2
    {
        public static bool IsValid(string? input)
        {
            return input != null;

            throw new NotImplementedException();
        }
    }
}
