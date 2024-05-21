using DayStory.Application.DTOs;
using DayStory.Domain.Enums;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators;

public class UserContractValidator : AbstractValidator<UserContract>
{
    public UserContractValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.LastName)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Username)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Email)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .EmailAddress().WithMessage("{PropertyName} must be in correct format.");

        RuleFor(user => user.Password)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .MinimumLength(10).WithMessage("{PropertyName} must be at least ten characters.")
            .Matches(@"^(?=.*[A-Z])(?=.*\d).+$").WithMessage("{PropertyName} must contain at least one uppercase letter and one digit");

        RuleFor(user => user.BirthDate)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date in the format dd-MM-yyyy.");

        RuleFor(user => user.Gender)
            .NotNull().NotEmpty().WithMessage("{PropertyName} required.")
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
