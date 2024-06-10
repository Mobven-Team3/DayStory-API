using System.Net;

namespace DayStory.Domain.Exceptions;

public class DaySummaryDateException : BaseException
{
    public DaySummaryDateException(string property) : base($"Creating daySummary can only be done in yesterday's events. {property} entered date is not valid.", HttpStatusCode.Conflict)
    {

    }
}