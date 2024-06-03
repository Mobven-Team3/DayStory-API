using DayStory.Common.DTOs;
using DayStory.Common.Enums;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators;

public class UserUpdateContractValidator : AbstractValidator<UpdateUserContract>
{
    public UserUpdateContractValidator()
    {
        RuleFor(user => user.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(2, 50).WithMessage("{PropertyName} must be between 2 and 50 characters.");

        RuleFor(user => user.LastName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(2, 50).WithMessage("{PropertyName} must be between 2 and 50 characters.");

        RuleFor(user => user.Username).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .EmailAddress().WithMessage("{PropertyName} must be in correct format.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

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
