using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Domain.Exceptions;
using DayStory.Application.Helper;

namespace DayStory.Application.Services;

public class DaySummaryService : BaseService<DaySummary, DaySummaryContract>, IDaySummaryService
{
    private readonly IDaySummaryRepository _daySummaryRepository;
    private readonly IEventService _eventService;
    private readonly IArtStyleService _artStyleService;
    private readonly IMapper _mapper;
    private readonly IOpenAIService _openAIService;
    public DaySummaryService(
        IGenericRepository<DaySummary, DaySummaryContract> repository, 
        IMapper mapper, 
        IDaySummaryRepository daySummaryRepository, 
        IEventService eventService, 
        IArtStyleService artStyleService, 
        IOpenAIService openAIService) : base(repository, mapper)
    {
        _daySummaryRepository = daySummaryRepository;
        _mapper = mapper;
        _eventService = eventService;
        _artStyleService = artStyleService;
        _openAIService = openAIService;
    }

    public async Task AddDaySummaryAsync(CreateDaySummaryContract model)
    {
        await CheckDaySummaryExistsAsync(model.Date, model.UserId);

        var createdModel = await GenerateDaySummaryContract(model);

        var daySummaryEntity = _mapper.Map<DaySummary>(createdModel);
        await _daySummaryRepository.AddDaySummaryAsync(daySummaryEntity);
    }

    public async Task<List<GetDaySummaryContract>> GetDaySummariesAsync(int userId)
    {
        var response = await _daySummaryRepository.GetDaySummariesByUserIdAsync(userId);
        return (response == null) ? throw new DaySummaryNotFoundWithGivenUserIdException(userId.ToString()) : _mapper.Map<List<GetDaySummaryContract>>(response);
    }

    public async Task<GetDaySummaryContract> GetDaySummaryByIdAsync(int id, int userId)
    {
        var entity = await _daySummaryRepository.GetByIdAsync(id);
        return (entity == null || entity.UserId != userId) ? throw new DaySummaryNotFoundException(id.ToString()) : _mapper.Map<GetDaySummaryContract>(entity);
    }

    public async Task<List<GetDaySummaryContract>> GetDaySummariesByMonthAsync(GetDaySummariesByMonthContract model)
    {
        var response = await _daySummaryRepository.GetDaySummariesByMonthAsync(model.Year, model.Month, (int)model.UserId);
        return (response != null) ? _mapper.Map<List<GetDaySummaryContract>>(response) : throw new DaySummaryNotFoundWithGivenDateException(model.Month);
    }

    public async Task<GetDaySummaryContract> GetDaySummaryByDayAsync(GetDaySummaryByDayContract model)
    {
        var response = await _daySummaryRepository.GetDaySummaryByDayAsync(model.Date, (int)model.UserId);
        return (response != null) ? _mapper.Map<GetDaySummaryContract>(response) : throw new DaySummaryNotFoundWithGivenDateException(model.Date);
    }

    #region Private Helper Methods
    private async Task<DaySummaryContract> GenerateDaySummaryContract(CreateDaySummaryContract model)
    {
        var createdModel = _mapper.Map<DaySummaryContract>(model);

        createdModel.Events = await GetEventsOnDateAsync(createdModel.Date, createdModel.UserId);

        var randomArtStyle = await GetRandomArtStyleAsync();
        createdModel.ArtStyleId = randomArtStyle.Id;

        var summary = await CreateDaySummaryTextAsync(createdModel.Events);

        createdModel.ImagePath = await GenerateAndSaveImageAsync(summary, randomArtStyle.Name, createdModel.Date, createdModel.UserId);

        createdModel.Summary = DaySummaryHelper.TrimSummaryToMaxLength(summary, 500);

        return createdModel;
    }

    private async Task CheckDaySummaryExistsAsync(string date, int userId)
    {
        var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(date, userId);

        if (existingDaySummary != null)
            throw new DaySummaryAlreadyExistsException(date);
    }

    private async Task<List<GetEventContract>> GetEventsOnDateAsync(string date, int userId)
    {
        var events = await _eventService.GetEventsByDayAsync(new GetEventsByDayContract()
        {
            Date = date,
            UserId = userId,
        });

        return (!events.Any()) ? throw new EventNotFoundWithGivenDateException(date) : events;
    }

    private async Task<ArtStyleContract> GetRandomArtStyleAsync()
    {
        var randomArtStyle = await _artStyleService.GetRandomArtStyleIdAsync();
        return (randomArtStyle == null) ? throw new InvalidOperationException("Failed to retrieve a random art style.") : randomArtStyle;
    }

    private async Task<string> CreateDaySummaryTextAsync(List<GetEventContract> events)
    {
        var eventsText = string.Join("\n", events.Select(e => $"{e.Title}: {e.Description}"));
        return await _openAIService.GetSummaryAsync(eventsText);
    }

    private async Task<string> GenerateAndSaveImageAsync(string summary, string artStyle, string date, int? userId)
    {
        var imageBytes = await _openAIService.GenerateImageAsync(summary, artStyle);
        return DaySummaryHelper.SaveImage(imageBytes, date, userId);
    }
    #endregion
}
