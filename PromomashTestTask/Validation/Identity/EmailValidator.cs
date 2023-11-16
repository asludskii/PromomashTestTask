using System.Text.RegularExpressions;
using FluentValidation;

namespace PromomashTestTask.Validation.Identity;

/// <summary>
/// Validates emails reasonably well, I hope.
/// </summary>
public class EmailValidator : AbstractValidator<string>
{
    private static Regex _emailRegex = new(
        "^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$",
        RegexOptions.Compiled);

    public EmailValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("Email can not be empty.");

        RuleFor(x => x.Length)
            .GreaterThanOrEqualTo(3) // minimum conceivable email length a@b
            .WithMessage("Email has to be at least 3 characters long.")
            .LessThanOrEqualTo(255) // max email length is 255 chars
            .WithMessage("Email has to be at most 255 characters long.");

        // Most expensive check goes last
        RuleFor(x => x)
            .Must(x => _emailRegex.IsMatch(x))
            .WithMessage("Invalid email format.");
    }
}