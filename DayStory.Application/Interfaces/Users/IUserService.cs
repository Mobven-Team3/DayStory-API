using DayStory.Application.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Interfaces;

public interface IUserService : IBaseService<User, UserContract>
{
    Task<string> LoginUserAsync(UserLoginContract requestModel);
    Task RegisterUserAsync(UserContract requestModel);
}
