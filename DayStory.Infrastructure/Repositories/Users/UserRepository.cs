using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<User>();
    }

    public async Task<User> UserCheckAsync(string email)
    {
        var user = await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        if (user != null)
        {
            return user;
        }
        else
            throw new UserNotFoundException(email);
    }

    public async Task<bool> UsernameCheckAsync(string username)
    {
        var userCheck = await _dbSet.FirstOrDefaultAsync(x => x.Username == username);
        return userCheck != null ? true : false;
    }
}
