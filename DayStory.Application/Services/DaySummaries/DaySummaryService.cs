using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Domain.Exceptions;
using System.Globalization;

namespace DayStory.Application.Services;

public class DaySummaryService : BaseService<DaySummary, DaySummaryContract>, IDaySummaryService
{
    private readonly IDaySummaryRepository _daySummaryRepository;
    private readonly IEventService _eventService;
    private readonly IArtStyleService _artStyleService;
    private readonly IMapper _mapper;
    private readonly OpenAIService _openAIService;
    public DaySummaryService(IGenericRepository<DaySummary, DaySummaryContract> repository, IMapper mapper, IDaySummaryRepository daySummaryRepository, IEventService eventService, IArtStyleService artStyleService, OpenAIService openAIService) : base(repository, mapper)
    {
        _daySummaryRepository = daySummaryRepository;
        _mapper = mapper;
        _eventService = eventService;
        _artStyleService = artStyleService;
        _openAIService = openAIService;
    }

    public async Task<DaySummaryContract> AddDaySummaryAsync(CreateDaySummaryContract model)
    {
        // Check if the date is today
        EnsureDateIsToday(model.Date);

        // Checks if there is a daysummary created
        var existingDaySummary = await _daySummaryRepository.GetDaySummaryByDayAsync(model.Date, model.UserId);
        if (existingDaySummary != null)
            throw new DaySummaryAlreadyExistsException(model.Date);

        var createdModel = _mapper.Map<DaySummaryContract>(model);

        // Get Events on the entered date
        createdModel.Events = await _eventService.GetEventsByDayAsync(new GetEventsByDayContract()
        {
            Date = createdModel.Date,
            UserId = createdModel.UserId,
        });

        if (createdModel.Events == null || !createdModel.Events.Any())
            throw new EventNotFoundWithGivenDateException(model.Date);

        // Get random theme
        var randomArtStyle = await _artStyleService.GetRandomArtStyleIdAsync();
        if (randomArtStyle == null)
            throw new InvalidOperationException("Failed to retrieve a random art style.");
        
        createdModel.ArtStyleId = randomArtStyle.Id;

        // Sending events to AI and creating a day summary
        var eventsText = string.Join("\n", createdModel.Events.Select(e => $"{e.Title}: {e.Description}"));
        var summary = await _openAIService.GetSummaryAsync(eventsText);

        // Sending summary to AI and creating a image
        var imageBytes = await _openAIService.GenerateImageAsync(summary, randomArtStyle.Name);

        // Save image to path
        var imagePath = SaveImage(imageBytes, createdModel.Date, createdModel.UserId);
        createdModel.ImagePath = imagePath;

        // Characters are limited to 500
        summary = (summary.Length > 500) ? summary.Substring(0, 497) + "..." : summary;

        createdModel.Summary = summary.Trim();

        var daySummaryEntity = _mapper.Map<DaySummary>(createdModel);
        await _daySummaryRepository.AddDaySummaryAsync(daySummaryEntity);

        return createdModel;
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
            return _mapper.Map<List<GetDaySummaryContract>>(response);
        }
        else
            throw new ArgumentNullException(nameof(model));
    }

    public async Task<GetDaySummaryContract> GetDaySummaryByDayAsync(GetDaySummaryByDayContract model)
    {
        if (model != null)
        {
            var response = await _daySummaryRepository.GetDaySummaryByDayAsync(model.Date, (int)model.UserId);
            return _mapper.Map<GetDaySummaryContract>(response);
        }
        else
            throw new ArgumentNullException(nameof(model));
    }

    private void EnsureDateIsToday(string dateString)
    {
        if (!DateTime.TryParseExact(dateString, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
        {
            throw new InvalidDaySummaryDateException(dateString);
        }

        if (date.Date != DateTime.UtcNow.Date)
        {
            throw new DaySummaryDateException(dateString);
        }
    }
}
