using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IEventRepository : IGenericRepository<Event, EventContract>
{
    Task<List<Event>> GetAllEventsByUserIdAsync(int userId);
}
