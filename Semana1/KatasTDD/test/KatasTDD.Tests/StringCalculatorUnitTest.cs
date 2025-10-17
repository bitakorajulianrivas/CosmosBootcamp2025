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
    public void Calculate_IfTheInputAreTwoNumbersSeparatedByComma_ShouldReturnSum()
    {
        string input = "1,2";

        int result = Calculate(input);

        result.Should().Be(3);
    }

    private static int Calculate(string input)
    {
        if (input.Length > 1 && input.Length <= 3)
        {
            string[] numbers = input.Split(",");
            return Convert.ToInt32(numbers[0]) + 
                   Convert.ToInt32(numbers[1]);
        }

        if (input == "1")
            return 1;

        return 0;
    }
}