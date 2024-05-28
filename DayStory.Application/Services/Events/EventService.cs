using AutoMapper;
using Azure;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Pagination;
using DayStory.Domain.Repositories;
using System.Globalization;

namespace DayStory.Application.Services;

public class EventService : BaseService<Event, EventContract>, IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public EventService(IGenericRepository<Event, EventContract> repository, IMapper mapper, IEventRepository eventRepository) : base(repository, mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task AddEventAsync(EventCreateContract model)
    {
        var entity = _mapper.Map<Event>(model);
        if(entity != null)
            await _eventRepository.AddAsync(entity);
        else
            throw new ArgumentNullException(nameof(model));
    }

    public async Task<List<EventGetContract>> GetAllEventAsync(int userId)
    {
        var response = await _eventRepository.GetAllEventsByUserIdAsync(userId);
        if (response == null)
            throw new ArgumentNullException(nameof(response));
        else
            return _mapper.Map<List<EventGetContract>>(response);
    }

    public async Task<EventGetContract> GetEventByIdAsync(int id)
    {
        var response = await _eventRepository.GetByIdAsync(id);
        if (response == null)
            throw new ArgumentNullException(nameof(response));
        else
            return _mapper.Map<EventGetContract>(response);
    }

    public async Task RemoveEventByIdAsync(int id)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        if (entity == null)
            throw new EventNotFoundException(id.ToString());

        if (DateTime.TryParseExact(entity.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime eventDate))
        {
            if (eventDate >= DateTime.Today)
                await _eventRepository.RemoveByIdAsync(id);
            else
                throw new InvalidEventDateException(eventDate.ToString("dd-MM-yyyy"));
        }
        else
            throw new InvalidEventDateException(entity.Date);
    }

    public async Task UpdateEventAsync(EventUpdateContract model)
    {
        var entity = _mapper.Map<EventContract>(model);
        await _eventRepository.UpdateAsync(entity);
    }

    //public Task<PagedResponse<EventGetContract>> GetPagedEventAsync(int pageNumber, int pageSize)
    //{
    //    throw new NotImplementedException();
    //}
}
