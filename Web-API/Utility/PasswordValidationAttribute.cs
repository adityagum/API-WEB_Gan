using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Web_API.Utility;

public class PasswordValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        var password = (string) value;
        if (password == null) return false;

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        var hasMinimum6Chars = new Regex(@".{6,}");

        var isValidated = hasNumber.IsMatch(password)
                      && hasUpperChar.IsMatch(password)
                      && hasLowerChar.IsMatch(password)
                      && hasSymbols.IsMatch(password)
                      && hasMinimum6Chars.IsMatch(password);

        return isValidated;
    }

}
