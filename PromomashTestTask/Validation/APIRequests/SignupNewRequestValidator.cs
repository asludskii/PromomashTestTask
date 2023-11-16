using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PromomashTestTask.Data;
using PromomashTestTask.DTOs.APIRequests;
using PromomashTestTask.Validation.Identity;

namespace PromomashTestTask.Validation.APIRequests;

public class SignupNewRequestValidator : AbstractValidator<SignupNewRequestDto>
{
    public SignupNewRequestValidator(AppDbContext dbContext)
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .SetValidator(new EmailValidator());

        RuleFor(x => x.Password)
            .NotEmpty()
            .SetValidator(new PasswordValidator());

        RuleFor(x => x.PolicyAccepted)
            .NotEmpty().WithMessage("Must agree to work for food.")
            .Equal(true).WithMessage("Must agree to work for food.");

        RuleFor(x => x.ProvinceId)
            .NotEmpty().WithMessage("Province must be selected.");
    }
}