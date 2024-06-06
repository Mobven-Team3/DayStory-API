using DayStory.Common.DTOs;
using DayStory.Common.Enums;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators;

public class UserRegisterContractValidator : AbstractValidator<RegisterUserContract>
{
    public UserRegisterContractValidator()
    {
        RuleFor(user => user.FirstName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(2, 50).WithMessage("{PropertyName} must be between 2 and 50 characters.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{PropertyName} must contain only letters.");

        RuleFor(user => user.LastName).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(2, 50).WithMessage("{PropertyName} must be between 2 and 50 characters.")
            .Matches(@"^[a-zA-Z]+$").WithMessage("{PropertyName} must contain only letters.");

        RuleFor(user => user.Username).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Email).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .EmailAddress().WithMessage("{PropertyName} must be in correct format.")
            .Length(3, 50).WithMessage("{PropertyName} must be between 3 and 50 characters.")
            .Must(NotContainTurkishCharacters).WithMessage("{PropertyName} must not contain Turkish characters.");

        RuleFor(user => user.Password).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(7, 50).WithMessage("{PropertyName} must be between 7 and 50 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{7,50}$").WithMessage("{PropertyName} must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");

        RuleFor(user => user.PasswordConfirmed).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Equal(user => user.Password).WithMessage("Passwords do not match.");

        RuleFor(user => user.BirthDate).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date in the format dd-MM-yyyy.")
            .Must(BeAValidBirthYear).WithMessage("{PropertyName} must be between 5 and 100 years old.");

        RuleFor(user => user.Gender).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidGender).WithMessage("{PropertyName} must be a valid gender.");
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    //private bool BeAValidAge(string date)
    //{
    //    if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
    //    {
    //        var age = DateTime.Now.Year - birthDate.Year;
    //        if (birthDate > DateTime.Now.AddYears(-age)) age--;
    //        return age >= 5 && age <= 100;
    //    }
    //    return false;
    //}

    private bool BeAValidBirthYear(string date)
    {
        if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
        {
            return birthDate.Year >= 1924 && birthDate.Year <= 2019;
        }
        return false;
    }

    private bool BeAValidGender(string gender)
    {
        return Enum.TryParse(typeof(Gender), gender, true, out _);
    }

    private bool NotContainTurkishCharacters(string email)
    {
        var turkishCharacters = new[] { 'ç', 'ğ', 'ı', 'ö', 'ş', 'ü', 'Ç', 'Ğ', 'İ', 'Ö', 'Ş', 'Ü' };
        return !email.Any(turkishCharacters.Contains);
    }
}
