﻿using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Domain.Exceptions;
using Microsoft.AspNetCore.Components.Forms;

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
        //EnsureDateIsPreviousDay(model.Date);

        var createdModel = _mapper.Map<DaySummaryContract>(model);
        createdModel.Events = await _eventService.GetEventsByDayAsync(new GetEventsByDayContract()
        {
            Date = createdModel.Date,
            UserId = createdModel.UserId,
        });

        if (createdModel.Events == null || !createdModel.Events.Any())
            throw new EventNotFoundWithGivenDateException(model.Date);

        // mehtod adı düzenle
        var randomArtStyle = await _artStyleService.GetRandomArtStyleIdAsync();
        if (randomArtStyle == null)
            throw new InvalidOperationException("Failed to retrieve a random art style.");
        
        createdModel.ArtStyleId = (int)randomArtStyle.Id;

        // ChatGPT ile özet alma
        var eventsText = string.Join("\n", createdModel.Events.Select(e => $"{e.Title}: {e.Description}"));
        var summary = await _openAIService.GetSummaryAsync(eventsText);

        // DALL-E ile görsel oluşturma
        var imageBytes = await _openAIService.GenerateImageAsync(summary, randomArtStyle.Name);

        // Görseli kaydetme
        var imagePath = SaveImage(imageBytes, createdModel.Date, createdModel.UserId);
        createdModel.ImagePath = imagePath;

        // Özetin boyutunu kontrol etme ve kesme
        if (summary.Length > 500)
        {
            summary = summary.Substring(0, 497) + "...";
        }

        createdModel.Summary = summary.Trim();

        // Entity'yi veritabanına kaydetme
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

    private void EnsureDateIsPreviousDay(string dateString)
    {
        if (!DateTime.TryParse(dateString, out var date))
        {
            throw new InvalidDaySummaryDateException(dateString);
        }

        if (date.Date != DateTime.UtcNow.Date.AddDays(-1))
        {
            throw new DaySummaryDateException(dateString);
        }
    }
}
