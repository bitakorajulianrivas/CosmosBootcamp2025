namespace KatasTDD.Domain.PasswordValidators;

public class PasswordValidator1 : PasswordValidator
{
    public override bool IsValid(string input)
    {
        return IsNotNullAndNotEmpty(input) &&
               IsMoreThanEightCharacters(input) &&
               ContainsCapitalLetters(input) &&
               ContainsLowerCaseLetters(input)  &&
               ContainsDigits(input) &&
               ContainsUnderscores(input);
    }

    private bool IsMoreThanEightCharacters(string input) => input.Length > 8;
}