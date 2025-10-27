using System.Text.RegularExpressions;

namespace KatasTDD.Domain.StringsCalculator;

public static class StringCalculator
{
    public static int Calculate(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return 0;
        
        return SumParsedNumbers(input);
    }

    private static int SumParsedNumbers(string input)
    {
        int[] integers = ExtractIntegers(input);

        VerifyIfExistAnyNegative(integers);

        return integers.Sum();
    }

    private static int[] ExtractIntegers(string input)
    {
        string onlyIntegersPattern = @"[-+]?\d+";

        int[] numbers = Regex.Matches(input, onlyIntegersPattern)
            .Select(match => int.Parse(match.Value))
            .Where(number => number <= 1000)
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