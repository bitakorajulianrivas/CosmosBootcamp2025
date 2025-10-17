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

    private static int Calculate(string input)
    {
        return 0;
    }
}