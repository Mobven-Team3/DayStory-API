using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User, UserContract>
{
    Task<User> UserCheckAsync(string email);
    Task<bool> UsernameCheckAsync(string username);
}
