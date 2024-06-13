using DayStory.Common.DTOs;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators.DaySummaryValidators;

public class CreateDaySummaryContractValidator : AbstractValidator<CreateDaySummaryContract>
{
    public CreateDaySummaryContractValidator()
    {
        RuleFor(x => x.Date).Cascade(CascadeMode.StopOnFirstFailure)
            .NotNull().WithMessage("{PropertyName} required.")
            .NotEmpty().WithMessage("{PropertyName} required.")
            .Must(BeAValidDate).WithMessage("{PropertyName} must be a valid date in the format dd-MM-yyyy.")
            .Must(BeToday).WithMessage("{PropertyName} must be today."); ;
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
}
