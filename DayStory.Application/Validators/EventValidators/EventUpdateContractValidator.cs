using DayStory.Common.DTOs;
using DayStory.Common.Enums;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators;

public class EventUpdateContractValidator : AbstractValidator<EventUpdateContract>
{
    public EventUpdateContractValidator()
    {
        RuleFor(user => user.Title).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Length(3, 250).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Description).Cascade(CascadeMode.StopOnFirstFailure)
            .Length(3, 350).WithMessage("{PropertyName} must be between 3 and 50 characters.");

        RuleFor(user => user.Date).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date in the format dd-MM-yyyy.")
            .Must(BeTodayOrFutureDate).WithMessage("{PropertyName} cannot be a past date.");

        RuleFor(user => user.Time).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(BeAValidTime).When(x => x.Time != null).WithMessage("{PropertyName} must be a valid time in the format HH:mm.");

        RuleFor(user => user.Priority).Cascade(CascadeMode.StopOnFirstFailure)
            .Must(BeAValidPriority).When(x => x.Time != null).WithMessage("{PropertyName} must be a valid priority.");
    }
    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }

    private bool BeTodayOrFutureDate(string date)
    {
        if (DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate.Date >= DateTime.Today;
        }
        return false;
    }

    private bool BeAValidTime(string time)
    {
        return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None, out _);
    }

    private bool BeAValidPriority(string priority)
    {
        return Enum.TryParse(typeof(Priority), priority, true, out _);
    }
}
