using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;
using System.Diagnostics.Contracts;

namespace DayStory.Application.Interfaces;

public interface IEventService : IBaseService<Event, EventContract>
{
    Task AddEventAsync(CreateEventContract model);
    Task RemoveEventByIdAsync(int id);
    Task UpdateEventAsync(UpdateEventContract model);
    Task<List<GetEventContract>> GetEventsAsync(int userId);
    Task<GetEventContract> GetEventByIdAsync(int id);
    Task<List<GetEventContract>> GetEventsByDayAsync(string date, int userId);
    Task<List<GetEventContract>> GetEventsByMonthAsync(string year, string month, int userId);

    //Task<PagedResponse<EventGetContract>> GetPagedEventAsync(int pageNumber, int pageSize);
}
