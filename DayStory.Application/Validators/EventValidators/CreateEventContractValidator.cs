using DayStory.Common.DTOs;
using DayStory.Common.Enums;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators;

public class CreateEventContractValidator : AbstractValidator<CreateEventContract>
{
    public CreateEventContractValidator()
    {
        RuleFor(x => x.Title).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 250).WithMessage("{PropertyName} must be between 3 and 250 characters.");
        
        RuleFor(x => x.Description).Cascade(CascadeMode.StopOnFirstFailure)
            .Length(3, 350).WithMessage("{PropertyName} must be between 3 and 350 characters.");

        RuleFor(x => x.Date).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date in the format dd-MM-yyyy.")
            .Must(BeToday).WithMessage("{PropertyName} must be today.");

        RuleFor(x => x.Time).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(BeAValidTime).When(x => x.Time != null).WithMessage("{PropertyName} must be a valid time in the format HH:mm.");

        RuleFor(x => x.Priority).Cascade(CascadeMode.StopOnFirstFailure)
            .IsInEnum().When(x => x.Time != null).WithMessage("{PropertyName} must be a valid priority.")
            .When(x => x.Priority != 0);
    }
    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    private bool BeToday(string date)
    {
        if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate.Date == DateTime.Today;
        }
        return false;
    }

    private bool BeAValidTime(string? time)
    {
        return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out _);
    }
}
