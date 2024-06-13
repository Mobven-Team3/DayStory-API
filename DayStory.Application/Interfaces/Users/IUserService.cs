using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Interfaces;

public interface IUserService : IBaseService<User, UserContract>
{
    Task<LoginUserResponseContract> LoginUserAsync(LoginUserContract requestModel);
    Task RegisterUserAsync(RegisterUserContract requestModel);
    Task UpdateUserAsync(UpdateUserContract requestModel);
    Task UpdatePasswordAsync(PasswordUpdateUserContract requestModel);
    Task<GetUserContract> GetUserAsync(int id);
}
