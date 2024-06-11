using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DayStory.Domain.Exceptions;

public class DaySummaryAlreadyExistsException : BaseException
{
    public DaySummaryAlreadyExistsException(string property) : base($"A daysummary for the date {property} already exists.", HttpStatusCode.Conflict)
    {

    }
}