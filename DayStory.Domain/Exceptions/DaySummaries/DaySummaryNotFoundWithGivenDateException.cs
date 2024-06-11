using System.Net;

namespace DayStory.Domain.Exceptions;

public class DaySummaryNotFoundWithGivenDateException : BaseException
{
    public DaySummaryNotFoundWithGivenDateException(string property) : base($"DaySummary not found with given Date: {property}", HttpStatusCode.Conflict)
    {

    }
}