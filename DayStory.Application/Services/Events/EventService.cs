using AutoMapper;
using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class EventService : BaseService<Event, EventContract>, IEventService
{
    public EventService(IGenericRepository<Event> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
