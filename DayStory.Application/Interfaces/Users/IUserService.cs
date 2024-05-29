using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Interfaces;

public interface IUserService : IBaseService<User, UserContract>
{
    Task<string> LoginUserAsync(LoginUserContract requestModel);
    Task RegisterUserAsync(RegisterUserContract requestModel);
    Task UpdateUserAsync(UpdateUserContract requestModel);
    Task UpdatePasswordAsync(PasswordUpdateUserContract requestModel);
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string password);
    Task<GetUserContract> GetUserAsync(int id);
}
