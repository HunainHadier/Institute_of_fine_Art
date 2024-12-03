using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Institute_of_Fine_Arts.Validations
{
    public class PassowrdValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {

                return new ValidationResult("Password is required");
            }


            var password = value as string;

            var hasnumber = new Regex(@"[0-9]+");

            var hasUppercase = new Regex(@"[A-Z]+");

            var hasLowercase = new Regex(@"[a-z]+");

            var hasMinCharacter = new Regex(@".{8,}");

            var hasSpecailcharacter = new Regex(@"[\W]+");



            if (!hasnumber.IsMatch(password)) {

                return new ValidationResult("Atleast 1 NumaricDigit");
            }
           else if(!hasUppercase.IsMatch(password)) {

                return new ValidationResult("Atleast 1 UpperCase");
            }
            else if (!hasLowercase.IsMatch(password)) {

                return new ValidationResult("Atleast 1 LowerCase");
            }
            else if (!hasMinCharacter.IsMatch(password))
            {

                return new ValidationResult("Min Use * Char");
            }
            else if (!hasSpecailcharacter.IsMatch(password))
            {

                return new ValidationResult("Atleast 1 SpecailCharacter");
            }
            return ValidationResult.Success;
        }

    }
}
