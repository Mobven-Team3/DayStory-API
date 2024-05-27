using DayStory.Common.DTOs;
using DayStory.Common.Enums;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators;

public class UserRegisterContractValidator : AbstractValidator<UserRegisterContract>
{
    public UserRegisterContractValidator()
    {
        RuleFor(user => user.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.LastName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Username).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .EmailAddress().WithMessage("{PropertyName} must be in correct format.");

        RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .MinimumLength(7).WithMessage("{PropertyName} must be at least 7 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{10,}$").WithMessage("{PropertyName} must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

        RuleFor(user => user.PasswordConfirmed).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Equal(user => user.Password).WithMessage("Passwords do not match.");

        RuleFor(user => user.BirthDate).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date in the format dd-MM-yyyy.");

        RuleFor(user => user.Gender).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidGender).WithMessage("{PropertyName} must be a valid gender.");
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    private bool BeAValidGender(string gender)
    {
        return Enum.TryParse(typeof(Gender), gender, true, out _);
    }
}
