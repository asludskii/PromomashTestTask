using Microsoft.AspNetCore.Identity;
using PromomashTestTask.Models;

namespace PromomashTestTask.Identity;

/// <summary>
/// Password validator.
/// Since there is a requirement which can't be exactly implemented by default identity config in Program.cs,
/// all validation logic is implemented here, to avoid fragmentation of closely related code.
/// </summary>
public class PasswordValidator : IPasswordValidator<ApplicationUser>
{
    private const int MinPasswordLength = 2;
    private const int MaxPasswordLength = 16;

    #region Implementation of IPasswordValidator<ApplicationUser>

    public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
    {
        var validationErrors = new Lazy<List<IdentityError>>();

        if (string.IsNullOrWhiteSpace(password)) 
            AddValidationError(validationErrors, "Password can not be empty.");

        if (!password.Any(char.IsLetter)) 
            AddValidationError(validationErrors, "Password must contain a letter.");

        if (!password.Any(char.IsDigit))
            AddValidationError(validationErrors, "Password must contain a digit.");

        if (MinPasswordLength > password.Length || password.Length > MaxPasswordLength)
            AddValidationError(validationErrors,
                $"Password must be between {MinPasswordLength} and {MaxPasswordLength} characters long.");

        return validationErrors.IsValueCreated && validationErrors.Value.Any()
            ? Task.FromResult(IdentityResult.Failed(validationErrors.Value.ToArray()))
            : Task.FromResult(IdentityResult.Success);
    }

    private static void AddValidationError(Lazy<List<IdentityError>> errors, string message)
    {
        errors.Value.Add(new IdentityError
        {
            Description = message
        });
    }

    #endregion
}