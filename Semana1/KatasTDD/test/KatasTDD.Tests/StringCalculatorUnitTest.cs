using FluentAssertions;

namespace KatasTDD.Tests;

public class StringCalculatorTest
{
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

    private static int Calculate(string input)
    {
        if (input == string.Empty)
            return 0;

        return input
            .Split(",")
            .Sum(Convert.ToInt32);
    }
}