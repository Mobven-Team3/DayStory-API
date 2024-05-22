using DayStory.Application.DTOs;
using FluentValidation;

namespace DayStory.Application.Validators;

public class UserLoginContractValidator : AbstractValidator<UserLoginContract>
{
    public UserLoginContractValidator()
    {
        RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .EmailAddress().WithMessage("{PropertyName} must be in correct format.");

        RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .MinimumLength(7).WithMessage("{PropertyName} must be at least ten characters.");
    }
}
