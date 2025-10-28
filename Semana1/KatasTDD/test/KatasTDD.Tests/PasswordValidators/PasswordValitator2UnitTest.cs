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

        [Fact]
        public void IsValid_IfTheInputIsEmplty_ReturnFalse()
        {
            string? input = string.Empty;

            bool isValid = PasswordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputIsLessToSixCaracters_ReturnFalse()
        {
            string input = "pass";

            bool isValid = PasswordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputIsEqualsToSixCaracters_ReturnFalse()
        {
            string input = "passwo";

            bool isValid = PasswordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputDoesNotContainACapitalLetter_ReturnFalse()
        {
            string input = "passwordwithoutcapitalletter";

            bool isValid = PasswordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }
        
        [Fact]
        public void IsValid_IfTheInputDoesNotContainsALowercaseLetter_returnFalse()
        {
            string input = "PASSWORDWITHOUTLOWERCASELETTER";

            bool isValid = PasswordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }
    }

    public class PasswordValidator2
    {
        public static bool IsValid(string? input)
        {
            if(string.IsNullOrEmpty(input))
                return false;

            if (input.Length <= 6)
                return false;

            if (input.Any(char.IsUpper) == false)
                return false;

            throw new NotImplementedException();
        }
    }
}
