using DayStory.Common.DTOs;
using FluentValidation;

namespace DayStory.Application.Validators;

public class UserLoginContractValidator : AbstractValidator<UserLoginContract>
{
    public UserLoginContractValidator()
    {
        RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.");

        RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.");
    }
}
