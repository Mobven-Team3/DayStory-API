using System.Net;

namespace DayStory.Domain.Exceptions;

public class EventNotFoundWithGivenUserIdException : BaseException
{
    public EventNotFoundWithGivenUserIdException(string property) : base($"Event not found with given User id: {property}", HttpStatusCode.Conflict)
    {

    }
}
