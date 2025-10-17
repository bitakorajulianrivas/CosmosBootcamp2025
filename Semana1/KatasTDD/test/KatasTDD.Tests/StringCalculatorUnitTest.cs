using FluentAssertions;

namespace KatasTDD.Tests;

public class StringCalculatorTest
{
    [Fact]
    public void Calculate_IfTheInputIsAnEmptyString_ShouldReturnZero()
    {
        var input = "";
        var result = Calculate(input);

        result.Should().Be(0);
    }

    private int Calculate(string input)
    {
        throw new NotImplementedException();
    }
}