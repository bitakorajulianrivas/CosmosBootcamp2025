using FluentAssertions;
using Xunit.Sdk;

namespace KatasTDD.Tests;

public class StringCalculatorTest
{
    [Fact]
    public void Calculate_IfTheInputIsNull_ShouldReturnZero()
    {
        string? input = null;

        int result = Calculate(input);

        result.Should().Be(0);
    }

    [Fact]
    public void Calculate_IfTheInputIsAnEmptyString_ShouldReturnZero()
    {
        string input = string.Empty;

        int result = Calculate(input);

        result.Should().Be(0);
    }

    [Fact]
    public void Calculate_IfTheInputIsOnlyOneNumber_ShouldReturnThatNumber()
    {
        string input = "1";

        int result = Calculate(input);

        result.Should().Be(1);
    }

    [Fact]
    public void Calculate_IfTheInputIsAnotherNumber_ShouldReturnThatNumber()
    {
        string input = "2";

        int result = Calculate(input);

        result.Should().Be(2);
    }

    [Fact]
    public void Calculate_IfTheInputAreTwoNumbersSeparatedByComma_ShouldReturnSum()
    {
        string input = "1,2";

        int result = Calculate(input);

        result.Should().Be(3);
    }

    [Fact]
    public void Calculate_IfTheInputAreMultipleNumbers_ShouldReturnSum()
    {
        string input = "1,2,3,4,5";

        int result = Calculate(input);

        result.Should().Be(15);
    }

    [Fact]
    public void Calculate_IfTheInputHasNewLinesOrCommas_ShouldReturnSum()
    {
        string input = "1\\n2,3";

        int result = Calculate(input);

        result.Should().Be(6);
    }

    [Fact]
    public void Calculate_IfTheInputHasNewLinesOrCommasJoinedTogether_ShouldReturnSum()
    {
        string input = "1,\\n2,3";

        int result = Calculate(input);

        result.Should().Be(6);
    }

    private static int Calculate(string? input)
    {
        string[] separators = ["\\n", ","];

        if (string.IsNullOrEmpty(input))
            return 0;

        return input
            .Split(separators, 
                StringSplitOptions.RemoveEmptyEntries)
            .Sum(int.Parse);
    }
}