using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class EventRepository : GenericRepository<Event, EventContract>, IEventRepository
{
    private readonly DbSet<Event> _dbSet;

    public EventRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<Event>();
    }

    public async Task<List<Event>> GetAllEventsByUserIdAsync(int userId)
    {
        var result = await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        if (result == null)
            throw new ArgumentNullException(typeof(IQueryable<Event>).ToString());
        else
           return result;
    }
}
