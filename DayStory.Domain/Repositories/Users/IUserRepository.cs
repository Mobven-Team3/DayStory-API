using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User, UserContract>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> UsernameCheckAsync(string username);
    Task SoftDeletedUserAddAsync(User user);
    Task UserLastLoginUpdateAsync(User user);
    Task<User?> UserCheckAndSoftDeletedUserAddAsync(string email);
}
