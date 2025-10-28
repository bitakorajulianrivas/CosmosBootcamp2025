namespace KatasTDD.Domain.PasswordValidators;

public class PasswordValidator3 : PasswordValidator
{
    public override bool IsValid(string input)
    {
        return IsNotNullAndNotEmpty(input) &&
               IsMoreThanSixteenCharacters(input) &&
               ContainsCapitalLetters(input) &&
               ContainsLowerCaseLetters(input) &&
               ContainsUnderscores(input);
    }

    private bool IsMoreThanSixteenCharacters(string input) => input.Length > 16;
}