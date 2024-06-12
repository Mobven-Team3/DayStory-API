using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Domain.Exceptions;

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

    public async Task<DaySummaryContract> AddDaySummaryAsync(CreateDaySummaryContract model)
    {
        await CheckDaySummaryExistsAsync(model.Date, model.UserId);

        var createdModel = _mapper.Map<DaySummaryContract>(model);

        // Get Events on the entered date
        createdModel.Events = await GetEventsOnDateAsync(createdModel.Date, createdModel.UserId);

        if (!createdModel.Events.Any())
            throw new EventNotFoundWithGivenDateException(model.Date);

        // Get random theme
        var randomArtStyle = await GetRandomArtStyleAsync();
        createdModel.ArtStyleId = randomArtStyle.Id;

        // Create day summary text
        var summary = await CreateDaySummaryTextAsync(createdModel.Events);

        // Generate image based on the summary and art style
        var imagePath = await GenerateAndSaveImageAsync(summary, randomArtStyle.Name, createdModel.Date, createdModel.UserId);
        createdModel.ImagePath = imagePath;

        // Limit summary characters to 500
        createdModel.Summary = TrimSummaryToMaxLength(summary, 500);

        var daySummaryEntity = _mapper.Map<DaySummary>(createdModel);
        await _daySummaryRepository.AddDaySummaryAsync(daySummaryEntity);

        return createdModel;
    }

    public async Task<List<GetDaySummaryContract>> GetDaySummariesAsync(int userId)
    {
        var response = await _daySummaryRepository.GetDaySummariesByUserIdAsync(userId);
        if (response == null)
            throw new DaySummaryNotFoundWithGivenUserIdException(userId.ToString());
        else
            return _mapper.Map<List<GetDaySummaryContract>>(response);
    }

    public async Task<GetDaySummaryContract> GetDaySummaryByIdAsync(int id, int userId)
    {
        var entity = await _daySummaryRepository.GetByIdAsync(id);
        if (entity == null || entity.UserId != userId)
            throw new DaySummaryNotFoundException(id.ToString());
        else
            return _mapper.Map<GetDaySummaryContract>(entity);
    }

    public async Task<List<GetDaySummaryContract>> GetDaySummariesByMonthAsync(GetDaySummariesByMonthContract model)
    {
        if (model != null)
        {
            var response = await _daySummaryRepository.GetDaySummariesByMonthAsync(model.Year, model.Month, (int)model.UserId);

            if (response != null)
                return _mapper.Map<List<GetDaySummaryContract>>(response);
            else
                throw new DaySummaryNotFoundWithGivenDateException(model.Month);
        }
        else
            throw new ArgumentNullException(nameof(model));
    }

    public async Task<GetDaySummaryContract> GetDaySummaryByDayAsync(GetDaySummaryByDayContract model)
    {
        if (model != null)
        {
            var response = await _daySummaryRepository.GetDaySummaryByDayAsync(model.Date, (int)model.UserId);

            if (response != null)
                return _mapper.Map<GetDaySummaryContract>(response);
            else
                throw new DaySummaryNotFoundWithGivenDateException(model.Date);
        }
        else
            throw new ArgumentNullException(nameof(model));
    }

    private async Task CheckDaySummaryExistsAsync(string date, int userId)
    {
        var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(date, userId);

        if (existingDaySummary != null)
        {
            throw new DaySummaryAlreadyExistsException(date);
        }
    }

    private async Task<List<GetEventContract>> GetEventsOnDateAsync(string date, int userId)
    {
        return await _eventService.GetEventsByDayAsync(new GetEventsByDayContract()
        {
            Date = date,
            UserId = userId,
        });
    }

    private async Task<ArtStyleContract> GetRandomArtStyleAsync()
    {
        var randomArtStyle = await _artStyleService.GetRandomArtStyleIdAsync();
        if (randomArtStyle == null)
            throw new InvalidOperationException("Failed to retrieve a random art style.");
        return randomArtStyle;
    }

    private async Task<string> CreateDaySummaryTextAsync(List<GetEventContract> events)
    {
        var eventsText = string.Join("\n", events.Select(e => $"{e.Title}: {e.Description}"));
        return await _openAIService.GetSummaryAsync(eventsText);
    }

    private async Task<string> GenerateAndSaveImageAsync(string summary, string artStyle, string date, int? userId)
    {
        var imageBytes = await _openAIService.GenerateImageAsync(summary, artStyle);
        return SaveImage(imageBytes, date, userId);
    }

    private string TrimSummaryToMaxLength(string summary, int maxLength)
    {
        return (summary.Length > maxLength) ? summary.Substring(0, maxLength - 3) + "..." : summary;
    }

    private string SaveImage(byte[] imageBytes, string date, int? userId)
    {
        var folderPath = Path.Combine("wwwroot", "images", "daysummaries", $"{userId}");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileName = $"{date.Replace("-", "")}_{Guid.NewGuid()}.png";
        var filePath = Path.Combine(folderPath, fileName);

        File.WriteAllBytes(filePath, imageBytes);

        return filePath;
    }
}
