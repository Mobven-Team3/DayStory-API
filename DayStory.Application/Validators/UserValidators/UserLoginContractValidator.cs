using DayStory.Application.DTOs;
using FluentValidation;

namespace DayStory.Application.Validators;

public class UserLoginContractValidator : AbstractValidator<UserLoginContract>
{
    public UserLoginContractValidator()
    {
        RuleFor(user => user.Email)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .EmailAddress().WithMessage("{PropertyName} must be in correct format.");

        RuleFor(user => user.Password)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .MinimumLength(10).WithMessage("{PropertyName} must be at least ten characters.")
            .Matches(@"^(?=.*[A-Z])(?=.*\d).+$").WithMessage("{PropertyName} must contain at least one uppercase letter and one digit");
    }
}
