using FluentAssertions;
using KatasTDD.Domain;

namespace KatasTDD.Tests;

public class StringCalculatorTest
{
    [Fact]
    public void Calculate_IfTheInputIsNull_ShouldReturnZero()
    {
        string? input = null;

        int result = StringCalculator.Calculate(input);

        result.Should().Be(0);
    }

    [Fact]
    public void Calculate_IfTheInputIsAnEmptyString_ShouldReturnZero()
    {
        string input = string.Empty;

        int result = StringCalculator.Calculate(input);

        result.Should().Be(0);
    }

    [Fact]
    public void Calculate_IfTheInputIsOnlyOneNumber_ShouldReturnThatNumber()
    {
        string input = "1";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(1);
    }

    [Fact]
    public void Calculate_IfTheInputIsAnotherNumber_ShouldReturnThatNumber()
    {
        string input = "2";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(2);
    }

    [Fact]
    public void Calculate_IfTheInputAreTwoNumbersSeparatedByComma_ShouldReturnSum()
    {
        string input = "1,2";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(3);
    }

    [Fact]
    public void Calculate_IfTheInputAreMultipleNumbers_ShouldReturnSum()
    {
        string input = "1,2,3,4,5";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(15);
    }

    [Fact]
    public void Calculate_IfTheInputHasNewLinesOrCommas_ShouldReturnSum()
    {
        string input = "1\\n2,3";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(6);
    }

    [Fact]
    public void Calculate_IfTheInputHasNewLinesOrCommasJoinedTogether_ShouldReturnSum()
    {
        string input = "1,\\n2,3";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(6);
    }

    [Fact]
    public void Calculate_IfTheInputHasCustomSeparators_ShouldGetCustomSeparatorAndReturnSum()
    {
        string input = "//;\\n1;2;3";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(6);
    }

    [Fact]
    public void Calculate_IfTheInputHasNegativeValues_ShouldThrowException()
    {
        string input = "1,-2,-3";

        Action action = () => StringCalculator.Calculate(input);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Negatives are not allowed: -2 -3.");
    }

    [Fact]
    public void Calculate_IfTheInputNumbersAreBiggerThan1000_ShouldIgnorethoseNumbersAndSumRemaining()
    {
        string input = "1001, 2";

        int result = StringCalculator.Calculate(input);

        result.Should().Be(2);
    }
}