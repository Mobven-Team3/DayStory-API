using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
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

    public async Task<List<Event>> GetEventsByUserIdAsync(int userId)
    {
        var result = await _dbSet.Where(x => x.UserId == userId).ToListAsync();

        return result;
    }

    public async Task<List<Event>> GetEventsByDayAsync(string date, int userId)
    {
        var result = await _dbSet.Where(x => x.Date ==  date && x.UserId == userId)
            .AsNoTracking()
            .OrderBy(x => x.Time)
            .ThenBy(x => x.Id)
            .ToListAsync();

        return result;
    }

    public async Task<List<Event>> GetEventsByMonthAsync(string year, string month, int userId)
    {
        var result = await _dbSet.Where(e => e.Date.Substring(6, 4) == year && e.Date.Substring(3, 2) == month && e.UserId == userId)
            .AsNoTracking()
            .OrderBy(x => x.Date)
            .ToListAsync();

        return result;
    }
}
