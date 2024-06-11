using System.Net;

namespace DayStory.Domain.Exceptions;

public class DaySummaryNotFoundWithGivenUserIdException : BaseException
{
    public DaySummaryNotFoundWithGivenUserIdException(string property) : base($"DaySummary not found with given User id: {property}", HttpStatusCode.Conflict)
    {

    }
}