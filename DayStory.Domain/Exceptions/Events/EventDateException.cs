using System.Net;

namespace DayStory.Domain.Exceptions;

public class EventDateException : BaseException
{
    public EventDateException(string property) : base($"Add, delete and update can only be done in today's events. {property} entered event date is not valid.", HttpStatusCode.Conflict)
    {

    }
}