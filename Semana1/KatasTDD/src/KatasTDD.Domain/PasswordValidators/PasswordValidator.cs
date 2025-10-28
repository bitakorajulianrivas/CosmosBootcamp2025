namespace KatasTDD.Domain.PasswordValidators;

public abstract class PasswordValidator
{
    public abstract bool IsValid(string input);

    protected bool IsNotNullAndNotEmpty(string input) => !string.IsNullOrEmpty(input);
    protected bool ContainsCapitalLetters(string input) => input.Any(char.IsUpper);
    protected bool ContainsLowerCaseLetters(string input) => input.Any(char.IsLower);
    protected bool ContainsDigits(string input) => input.Any(char.IsDigit);
}