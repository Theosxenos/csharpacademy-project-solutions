using System.ComponentModel.DataAnnotations;

namespace HabitLoggerMvc.Validators;

public class IsPositiveAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is >= 0;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (IsValid(value)) return ValidationResult.Success;

        var errorMessage = FormatErrorMessage(validationContext.DisplayName);
        return new ValidationResult(errorMessage);
    }

    public override string FormatErrorMessage(string name)
    {
        // Customize the error message here
        return $"{name} must not be negative.";
    }
}