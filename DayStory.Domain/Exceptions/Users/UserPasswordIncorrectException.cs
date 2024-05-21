using System.Net;

namespace DayStory.Domain.Exceptions;

public class UserPasswordIncorrectException : BaseException
{
    public UserPasswordIncorrectException(string property) : base($"Wrong password for user with email {property}.", HttpStatusCode.Unauthorized)
    {

    }
}
