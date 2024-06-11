using System.Net;

namespace DayStory.Domain.Exceptions;

public class DaySummaryNotFoundException : BaseException
{
    public DaySummaryNotFoundException(string property) : base($"DaySummary with id: {property} not found.", HttpStatusCode.Conflict)
    {

    }
}