namespace KatasTDD.Domain.PasswordValidators;

public static class PasswordValidator
{
    public static bool IsValid(string input)
    {
        return input.IsNotNullAndNotEmpty() &&
               input.IsMoreThanEightCharacters() &&
               input.ContainsCapitalLetters() &&
               input.ContainsLowerCaseLetters()  &&
               input.ContainsDigits() &&
               input.ContainsUnderscores();
    }

    private static bool IsNotNullAndNotEmpty(this string input) => !string.IsNullOrEmpty(input);
    private static bool IsMoreThanEightCharacters(this string input) => input.Length >= 8;
    private static bool ContainsCapitalLetters(this string input) => input.Any(char.IsUpper);
    private static bool ContainsLowerCaseLetters(this string input) => input.Any(char.IsLower);
    private static bool ContainsDigits(this string input) => input.Any(char.IsDigit);
    private static bool ContainsUnderscores(this string input) => input.Any(ch => ch == '_');
}