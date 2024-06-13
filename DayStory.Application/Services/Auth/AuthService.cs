using DayStory.Application.Interfaces;
using DayStory.Application.Options;
using DayStory.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DayStory.Application.Services;

public class AuthService : IAuthService
{
    private readonly JwtConfig _jwtConfig;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IOptionsMonitor<JwtConfig> jwtConfig, IPasswordHasher<User> passwordHasher)
    {
        _jwtConfig = jwtConfig.CurrentValue;
        _passwordHasher = passwordHasher;
    }

    public Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        return claims;
    }

    public string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            _jwtConfig.Issuer,
            _jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }

    public bool VerifyPassword(User user, string password)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }

    public string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }
}
