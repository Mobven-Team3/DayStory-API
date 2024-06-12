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
    public EventService(IGenericRepository<Event, EventContract> repository, IMapper mapper, IEventRepository eventRepository, IDaySummaryRepository daySummaryRepository) : base(repository, mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
        _daySummaryRepository = daySummaryRepository;
    }

    public async Task AddEventAsync(CreateEventContract model)
    {
        EnsureDateIsToday(model.Date);

        //var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(model.Date, model.UserId);

        //if (existingDaySummary != null)
        //{
        //    throw new DaySummaryAlreadyExistsException(model.Date);
        //}

        var entity = _mapper.Map<Event>(model);

        if(entity != null)
            await _eventRepository.AddAsync(entity);
        else
            throw new ArgumentNullException(nameof(model));
    }

    public async Task<List<GetEventContract>> GetEventsAsync(int userId)
    {
        var response = await _eventRepository.GetEventsByUserIdAsync(userId);

        if (response == null)
            throw new EventNotFoundWithGivenUserIdException(userId.ToString());
        else
            return _mapper.Map<List<GetEventContract>>(response);
    }

    public async Task<GetEventContract> GetEventByIdAsync(int id, int userId)
    {
        var entity = await _eventRepository.GetByIdAsync(id);

        if (entity == null || entity.UserId != userId)
            throw new EventNotFoundException(id.ToString());

        return _mapper.Map<GetEventContract>(entity);
    }

    public async Task<List<GetEventContract>> GetEventsByDayAsync(GetEventsByDayContract model)
    {
        if (model != null)
        {
            var response = await _eventRepository.GetEventsByDayAsync(model.Date, model.UserId);

            if (response != null)
                return _mapper.Map<List<GetEventContract>>(response);
            else
                throw new EventNotFoundWithGivenDateException(model.Date);
        }
        else
            throw new ArgumentNullException(nameof(model));
        
    }

    public async Task<List<GetEventContract>> GetEventsByMonthAsync(GetEventsByMonthContract model)
    {
        if (model != null)
        {
            var response = await _eventRepository.GetEventsByMonthAsync(model.Year, model.Month, model.UserId);

            if (response != null)
                return _mapper.Map<List<GetEventContract>>(response);
            else
                throw new EventNotFoundWithGivenDateException(model.Month);
        }
        else
            throw new ArgumentNullException(nameof(model));
    }

    public async Task RemoveEventByIdAsync(int id, int userId)
    {
        var entity = await _eventRepository.GetByIdAsync(id);

        if (entity == null || entity.UserId != userId)
            throw new EventNotFoundException(id.ToString());

        EnsureDateIsToday(entity.Date);

        //var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(entity.Date, userId);

        //if (existingDaySummary != null)
        //    throw new DaySummaryAlreadyExistsException(entity.Date);

        await _eventRepository.RemoveByIdAsync(id);
    }

    public async Task UpdateEventAsync(UpdateEventContract model)
    {
        EnsureDateIsToday(model.Date);

        //var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(model.Date, model.UserId);

        //if (existingDaySummary != null)
        //{
        //    throw new DaySummaryAlreadyExistsException(model.Date);
        //}

        var existCheck = await _eventRepository.GetByIdAsync(model.Id);

        if (existCheck != null && model.UserId == existCheck.UserId)
        {
            var entity = _mapper.Map<EventContract>(model);

            await _eventRepository.UpdateAsync(entity);
        }
        else
            throw new EventNotFoundException(model.Id.ToString());
    }

    private void EnsureDateIsToday(string date)
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
}
