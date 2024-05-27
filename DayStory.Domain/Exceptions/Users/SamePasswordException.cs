using System.Net;

namespace DayStory.Domain.Exceptions;

public class SamePasswordException : BaseException
{
    public SamePasswordException(string property) : base($"Same password for user with email {property}.", HttpStatusCode.Unauthorized)
    {
        
    }
}
