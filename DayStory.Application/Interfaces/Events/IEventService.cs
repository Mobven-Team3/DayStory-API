using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Pagination;
using System.Diagnostics.Contracts;

namespace DayStory.Application.Interfaces;

public interface IEventService : IBaseService<Event, EventContract>
{
    Task AddEventAsync(EventCreateContract model);
    Task RemoveEventByIdAsync(int id);
    Task UpdateEventAsync(EventUpdateContract model);
    Task<List<EventGetContract>> GetAllEventAsync(int userId);
    Task<EventGetContract> GetEventByIdAsync(int id);
    //Task<PagedResponse<EventGetContract>> GetPagedEventAsync(int pageNumber, int pageSize);
}
