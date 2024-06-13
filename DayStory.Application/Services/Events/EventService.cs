using AutoMapper;
using DayStory.Application.Interfaces;
using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class EventService : BaseService<Event, EventContract>, IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    private readonly IDaySummaryRepository _daySummaryRepository;
    public EventService(
        IGenericRepository<Event, EventContract> repository, 
        IMapper mapper, 
        IEventRepository eventRepository, 
        IDaySummaryRepository daySummaryRepository) : base(repository, mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _daySummaryRepository = daySummaryRepository;
    }

    public async Task AddEventAsync(CreateEventContract model)
    {
        await ValidateEventCreationAsync(model.Date, model.UserId);
        var entity = _mapper.Map<Event>(model);
        await _eventRepository.AddAsync(entity);
    }

    public async Task<List<GetEventContract>> GetEventsAsync(int userId)
    {
        var response = await _eventRepository.GetEventsByUserIdAsync(userId);
        return (response == null) ? throw new EventNotFoundWithGivenUserIdException(userId.ToString()) : _mapper.Map<List<GetEventContract>>(response);
    }

    public async Task<GetEventContract> GetEventByIdAsync(int id, int userId)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        return (entity == null || entity.UserId != userId) ? throw new EventNotFoundException(id.ToString()) : _mapper.Map<GetEventContract>(entity);
    }

    public async Task<List<GetEventContract>> GetEventsByDayAsync(GetEventsByDayContract model)
    {
        var response = await _eventRepository.GetEventsByDayAsync(model.Date, model.UserId);
        return (response != null) ? _mapper.Map<List<GetEventContract>>(response) : throw new EventNotFoundWithGivenDateException(model.Date);
    }

    public async Task<List<GetEventContract>> GetEventsByMonthAsync(GetEventsByMonthContract model)
    {
        var response = await _eventRepository.GetEventsByMonthAsync(model.Year, model.Month, model.UserId);
        return (response != null) ? _mapper.Map<List<GetEventContract>>(response) : throw new EventNotFoundWithGivenDateException(model.Month);
    }

    public async Task RemoveEventByIdAsync(int id, int userId)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        ValidateEventForRemoval(entity, id, userId);
        await _eventRepository.RemoveByIdAsync(id);
    }

    public async Task UpdateEventAsync(UpdateEventContract model)
    {
        await ValidateEventUpdateAsync(model);
        var entity = _mapper.Map<EventContract>(model);
        await _eventRepository.UpdateAsync(entity);
    }

    #region Private Helper Methods
    private async Task ValidateEventCreationAsync(string date, int userId)
    {
        await CheckDaySummaryExistsAsync(date, userId);
    }

    private void ValidateEventForRemoval(Event entity, int id, int userId)
    {
        if (entity == null || entity.UserId != userId)
            throw new EventNotFoundException(id.ToString());

        CheckDateIsToday(entity.Date);
        CheckDaySummaryExistsAsync(entity.Date, entity.UserId).Wait();
    }

    private async Task ValidateEventUpdateAsync(UpdateEventContract model)
    {
        await CheckDaySummaryExistsAsync(model.Date, model.UserId);
        var existingEvent = await _eventRepository.GetByIdAsync(model.Id);

        if (existingEvent == null || model.UserId != existingEvent.UserId)
            throw new EventNotFoundException(model.Id.ToString());
    }

    private void CheckDateIsToday(string date)
    {
        if (!DateTime.TryParseExact(date, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out var parsedDate))
        {
            throw new InvalidEventDateException(date);
        }

        if (parsedDate.Date != DateTime.Today)
        {
            throw new EventDateException(date);
        }
    }

    private async Task CheckDaySummaryExistsAsync(string date, int userId)
    {
        var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(date, userId);

        if (existingDaySummary != null)
            throw new DaySummaryAlreadyExistsException(date);
    }
    #endregion
}
