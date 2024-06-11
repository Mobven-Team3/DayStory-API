using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IEventRepository : IGenericRepository<Event, EventContract>
{
    Task<List<Event>> GetEventsByUserIdAsync(int userId);
    Task<List<Event>> GetEventsByDayAsync(string date, int userId);
    Task<List<Event>> GetEventsByMonthAsync(string year, string month, int userId);
}
