using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class EventRepository : GenericRepository<Event, EventContract>, IEventRepository
{
    private readonly DbSet<Event> _dbSet;

    public EventRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<Event>();
    }

    public async Task<List<Event>> GetEventsByUserIdAsync(int userId)
    {
        var result = await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        if (result == null)
            throw new ArgumentNullException(typeof(IQueryable<Event>).ToString());
        else
           return result;
    }

    public async Task<List<Event>> GetEventsByDayAsync(string date, int userId)
    {
        var spec = new EventsByDaySpecification(date, userId);
        return await FindAsync(spec);
    }

    public async Task<List<Event>> GetEventsByMonthAsync(string year, string month, int userId)
    {
        var spec = new EventsByMonthSpecification(year, month, userId);
        return await FindAsync(spec);
    }
}
