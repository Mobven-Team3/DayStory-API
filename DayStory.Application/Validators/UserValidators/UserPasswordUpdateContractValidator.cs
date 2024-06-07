using DayStory.Common.DTOs;
using FluentValidation;

namespace DayStory.Application.Validators;

public class UserPasswordUpdateContractValidator : AbstractValidator<PasswordUpdateUserContract>
{
    public UserPasswordUpdateContractValidator()
    {
        RuleFor(user => user.CurrentPassword).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(7, 50).WithMessage("{PropertyName} must be between 7 and 50 characters.");

        RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(7, 50).WithMessage("{PropertyName} must be between 7 and 50 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{7,50}$").WithMessage("{PropertyName} must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

        RuleFor(user => user.PasswordConfirmed).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Equal(user => user.Password).WithMessage("Passwords do not match.");
    }
}
