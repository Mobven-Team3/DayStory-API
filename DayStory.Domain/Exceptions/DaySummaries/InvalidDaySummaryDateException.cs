using System.Net;

namespace DayStory.Domain.Exceptions;

public class InvalidDaySummaryDateException : BaseException
{
    public InvalidDaySummaryDateException(string property) : base($"{property} entered daysummary date is not valid.", HttpStatusCode.Conflict)
    {

    }
}
