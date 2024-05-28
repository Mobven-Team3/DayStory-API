using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User, UserContract>, IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<User>();
    }

    public async Task SoftDeletedUserAddAsync(User user)
    {
        user.IsDeleted = false;
        await UpdateAsync(user);
    }

    public async Task<User> UserCheckAsync(string email)
    {
        var user = await _dbSet.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Email == email);
        if (user != null)
        {
            return user;
        }
        else
            return null;
    }

    public async Task UserLastLoginUpdateAsync(User user)
    {
        user.LastLogin = DateTime.UtcNow;
        await UpdateAsync(user);
    }

    public async Task<bool> UsernameCheckAsync(string username)
    {
        var userCheck = await _dbSet.FirstOrDefaultAsync(x => x.Username == username);
        if (userCheck != null)
        {
            userCheck.LastLogin = DateTime.UtcNow;
            await SaveAsync();
            return true;
        }
        else
            return false;
    }
}
