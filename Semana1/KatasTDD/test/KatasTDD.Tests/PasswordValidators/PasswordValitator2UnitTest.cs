using FluentAssertions;
using KatasTDD.Domain.PasswordValidators;

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
            if (input == null)
                return false;

            throw new NotImplementedException();
        }
    }
}
