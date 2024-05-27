using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Interfaces;

public interface IUserService : IBaseService<User, UserContract>
{
    Task<string> LoginUserAsync(UserLoginContract requestModel);
    Task RegisterUserAsync(UserRegisterContract requestModel);
    Task UpdateUserAsync(UserUpdateContract requestModel);
    Task UpdatePasswordAsync(UserPasswordUpdateContract requestModel);
    string HashPassword(User user, string password);
    bool VerifyPassword(User user, string password);
    Task<UserGetContract> GetUserAsync(int id);
}
