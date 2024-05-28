using System.Net;

namespace DayStory.Domain.Exceptions;

public class EventNotFoundException : BaseException
{
    public EventNotFoundException(string property) : base($"Evetn with id: {property} not found.", HttpStatusCode.Conflict)
    {
        
    }
}
