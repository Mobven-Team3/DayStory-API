using System.Net;

namespace DayStory.Domain.Exceptions;

public class EventNotFoundForUser : BaseException
{
    public EventNotFoundForUser(string property) : base($"No such event with id: {property} was found for the user.", HttpStatusCode.Conflict)
    {

    }
}