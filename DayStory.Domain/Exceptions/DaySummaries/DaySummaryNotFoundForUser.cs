using System.Net;

namespace DayStory.Domain.Exceptions;

public class DaySummaryNotFoundForUser : BaseException
{
    public DaySummaryNotFoundForUser(string property) : base($"No such daysummary with id: {property} was found for the user.", HttpStatusCode.Conflict)
    {

    }
}
