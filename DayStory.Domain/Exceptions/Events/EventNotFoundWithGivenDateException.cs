using System.Net;

namespace DayStory.Domain.Exceptions;

public class EventNotFoundWithGivenDateException : BaseException
{
    public EventNotFoundWithGivenDateException(string property) : base($"Event not found with given Date: {property}", HttpStatusCode.Conflict)
    {

    }
}