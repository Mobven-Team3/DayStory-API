using DayStory.Common.DTOs;
using FluentValidation;
using System.Globalization;

namespace DayStory.Application.Validators.DaySummaryValidators;

public class GetDaySummaryByDayContractValidator : AbstractValidator<GetDaySummaryByDayContract>
{
    public GetDaySummaryByDayContractValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Matches(@"^\d{2}-\d{2}-\d{4}$").WithMessage("Date must be in the format dd-MM-yyyy.")
            .Must(BeAValidDate).WithMessage("Date must be a valid date.");
    }
    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
