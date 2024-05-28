using System.Net;

namespace DayStory.Domain.Exceptions;

public class InvalidEventDateException : BaseException
{
    public InvalidEventDateException(string property) : base($"{property} entered event date is not valid.", HttpStatusCode.Conflict)
    {
        
    }
}
