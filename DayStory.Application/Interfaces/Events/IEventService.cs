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
    Task<List<GetEventContract>> GetEventsByDayAsync(GetEventsByDayContract model);
    Task<List<GetEventContract>> GetEventsByMonthAsync(GetEventsByMonthContract model);

    //Task<PagedResponse<EventGetContract>> GetPagedEventAsync(int pageNumber, int pageSize);
}
