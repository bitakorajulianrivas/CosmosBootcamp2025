namespace KatasTDD.Domain.PasswordValidators;

public class PasswordValidator2 : PasswordValidator
{
    public override bool IsValid(string input)
    {
        return IsNotNullAndNotEmpty(input) &&
               IsMoreThanSixCharacters(input) &&
               ContainsCapitalLetters(input) &&
               ContainsLowerCaseLetters(input) &&
               ContainsDigits(input);
    }

    private bool IsMoreThanSixCharacters(string input) => input.Length > 6;
}