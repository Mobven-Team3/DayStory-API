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
}
