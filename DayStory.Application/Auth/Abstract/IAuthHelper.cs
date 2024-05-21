using DayStory.Domain.Entities;
using System.Security.Claims;

namespace DayStory.Application.Auth;

public interface IAuthHelper
{
    string Token(User user);
    Claim[] GetClaims(User user);
}
