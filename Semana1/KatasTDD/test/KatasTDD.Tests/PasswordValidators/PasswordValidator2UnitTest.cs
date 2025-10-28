using FluentAssertions;
using KatasTDD.Domain.PasswordValidators;

namespace KatasTDD.Tests.PasswordValidators
{
    public class PasswordValidator2UnitTest
    {
        private readonly PasswordValidator _passwordValidator2;

        public PasswordValidator2UnitTest()
        {
            _passwordValidator2 = new PasswordValidator2();
        }

        [Fact]
        public void IsValid_IfTheInputIsNull_ReturnFalse()
        {
            string input = null;

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputIsEmplty_ReturnFalse()
        {
            string? input = string.Empty;

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputIsLessToSixCaracters_ReturnFalse()
        {
            string input = "pass";

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputIsEqualsToSixCaracters_ReturnFalse()
        {
            string input = "passwo";

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputDoesNotContainACapitalLetter_ReturnFalse()
        {
            string input = "passwordwithoutcapitalletter";

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }
        
        [Fact]
        public void IsValid_IfTheInputDoesNotContainsALowercaseLetter_returnFalse()
        {
            string input = "PASSWORDWITHOUTLOWERCASELETTER";

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfTheInputDoesNotContainANumber_ReturnFalse()
        {
            string input = "PassswordWithoutANumber";

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeFalse();
        }

        [Fact]
        public void IsValid_IfInputHasAllRules_ReturnTrue()
        {
            string input = "P4sssW0rdW1thNumb3rs";

            bool isValid = _passwordValidator2.IsValid(input);

            isValid.Should().BeTrue();
        }
    }
}
