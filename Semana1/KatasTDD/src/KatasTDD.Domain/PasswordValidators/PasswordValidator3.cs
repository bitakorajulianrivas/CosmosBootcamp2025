namespace KatasTDD.Domain.PasswordValidators;

public class PasswordValidator3 : PasswordValidator
{
    public override bool IsValid(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        if (input.Length <= 16)
            return false;

        if (input.Any(char.IsUpper) == false)
            return false;

        if (input.Any(char.IsLower) == false)
            return false;

        if (input.Any(ch => ch == '_') == false)
            return false;
    
        return true;
    }
}