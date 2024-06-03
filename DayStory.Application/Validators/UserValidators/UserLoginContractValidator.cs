using DayStory.Common.DTOs;
using FluentValidation;

namespace DayStory.Application.Validators;

public class UserLoginContractValidator : AbstractValidator<LoginUserContract>
{
    public UserLoginContractValidator()
    {
        RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.");
    }
}
