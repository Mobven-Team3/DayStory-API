using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> UserCheckAsync(string email);
    Task<bool> UsernameCheckAsync(string username);
}
