using DayStory.Domain.Entities;
using System.Security.Claims;

namespace DayStory.Application.Interfaces;

public interface IAuthService
{
    string Token(User user);
    Claim[] GetClaims(User user);
    bool VerifyPassword(User user, string password);
    string HashPassword(User user, string password);
}
