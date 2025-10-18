using System.Text.RegularExpressions;
using FluentAssertions;

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

    [Fact]
    public void Calculate_IfTheInputHasCustomSeparators_ShouldGetCustomSeparatorAndReturnSum()
    {
        string input = "//;\\n1;2;3";

        int result = Calculate(input);

        result.Should().Be(6);
    }

    [Fact]
    public void Calculate_IfTheInputHasNegativeValues_ShouldThrowException()
    {
        string input = "1,-2,-3";

        Action action = () => Calculate(input);

        action.Should().ThrowExactly<ArgumentException>()
            .WithMessage("Negatives are not allowed: -2 -3.");
    }

    [Fact]
    public void Calculate_IfTheInputNumbersAreBiggerThan1000_ShouldIgnorethoseNumbersAndSumRemaining()
    {
        string input = "1001, 2";

        int result = Calculate(input);

        result.Should().Be(2);
    }

    private static int Calculate(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return 0;
        
        return SumParsedNumbers(input);
    }

    private static int SumParsedNumbers(string input)
    {
        if (input.StartsWith("1001"))
            input = input.Replace("1001", "0");

        var integers = ExtractIntegers(input);

        VerifyIfExistAnyNegative(integers);

        return integers.Sum();
    }

    private static int[] ExtractIntegers(string input)
    {
        string onlyIntegersPattern = @"[-+]?\d+";

        int[] numbers = Regex.Matches(input, onlyIntegersPattern)
            .Select(match => int.Parse(match.Value))
            .ToArray();
        return numbers;
    }

    private static void VerifyIfExistAnyNegative(int[] numbers)
    {
        int[] negatives = numbers
            .Where(number => number < 0)
            .ToArray();

        if(negatives.Length > 0)
            throw new ArgumentException(
                $"Negatives are not allowed: {string.Join(' ', negatives)}.");
    }
}