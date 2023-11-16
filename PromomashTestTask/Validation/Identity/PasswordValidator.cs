using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PromomashTestTask.Models;

namespace PromomashTestTask.Validation.Identity;

/// <summary>
/// Password validator, used in both request DTO validation and identity.
/// Since there is a requirement which can't be exactly implemented by default identity config in Program.cs,
/// all validation logic is implemented here, to avoid fragmentation of closely related code.
/// </summary>
public class PasswordValidator : AbstractValidator<string>, IPasswordValidator<ApplicationUser>
{
    private const int MinPasswordLength = 2;
    private const int MaxPasswordLength = 16;

    public PasswordValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Password can not be empty.")
            .Must(x => x.Any(char.IsLetter)).WithMessage("Password must contain a letter.")
            .Must(x => x.Any(char.IsDigit)).WithMessage("Password must contain a digit.");

        RuleFor(x => x.Length)
            .GreaterThanOrEqualTo(MinPasswordLength)
            .LessThanOrEqualTo(MaxPasswordLength)
            .WithMessage($"Password must be between {MinPasswordLength} and {MaxPasswordLength} characters long.");
    }

    #region Implementation of IPasswordValidator<ApplicationUser>

    Task<IdentityResult> IPasswordValidator<ApplicationUser>.ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
    {
        var result = Validate(password);

        if (result.IsValid)
            return Task.FromResult(IdentityResult.Success);

        var identityErrors = result.Errors.Select(x => new IdentityError
        {
            Description = x.ErrorMessage
        });

        return Task.FromResult(IdentityResult.Failed(identityErrors.ToArray()));
    }

    #endregion
}