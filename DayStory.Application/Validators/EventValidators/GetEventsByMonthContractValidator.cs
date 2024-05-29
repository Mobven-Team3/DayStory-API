using DayStory.Common.DTOs;
using FluentValidation;

namespace DayStory.Application.Validators;

public class GetEventsByMonthContractValidator : AbstractValidator<GetEventsByMonthContract>
{
    public GetEventsByMonthContractValidator()
    {
        RuleFor(x => x.Year)
            .NotEmpty().WithMessage("Year is required.")
            .Matches(@"^\d{4}$").WithMessage("Year must be in the format yyyy.")
            .Must(BeAValidYear).WithMessage("Year must be a valid year.");

        RuleFor(x => x.Month)
            .NotEmpty().WithMessage("Month is required.")
            .Matches(@"^(0[1-9]|1[0-2])$").WithMessage("Month must be in the format MM.")
            .Must(BeAValidMonth).WithMessage("Month must be a valid month.");
    }
    private bool BeAValidYear(string year)
    {
        return int.TryParse(year, out int yearValue) && yearValue > 0;
    }

    private bool BeAValidMonth(string month)
    {
        return int.TryParse(month, out int monthValue) && monthValue >= 1 && monthValue <= 12;
    }
}
