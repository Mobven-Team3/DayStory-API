using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Repositories;

namespace DayStory.Application.Services;

public class DaySummaryService : BaseService<DaySummary, DaySummaryContract>, IDaySummaryService
{
    private readonly IDaySummaryRepository _daySummaryRepository;
    private readonly IEventService _eventService;
    private readonly IArtStyleService _artStyleService;
    private readonly IMapper _mapper;
    public DaySummaryService(IGenericRepository<DaySummary, DaySummaryContract> repository, IMapper mapper, IDaySummaryRepository daySummaryRepository, IEventService eventService, IArtStyleService artStyleService) : base(repository, mapper)
    {
        _daySummaryRepository = daySummaryRepository;
        _mapper = mapper;
        _eventService = eventService;
        _artStyleService = artStyleService;
    }

    public async Task<DaySummaryContract> AddDaySummaryAsync(CreateDaySummaryContract model)
    {
        var createdModel = _mapper.Map<DaySummaryContract>(model);
        createdModel.Events = await _eventService.GetEventsByDayAsync(new GetEventsByDayContract(){
                                                                            Date = createdModel.Date,
                                                                            UserId = createdModel.UserId,
                                                                        });

        var randomArtStyle = await _artStyleService.GetRandomArtStyleIdAsync();
        createdModel.ArtStyleId = (int)randomArtStyle.Id;


        throw new NotImplementedException();
    }

    public async Task<List<GetDaySummaryContract>> GetDaySummariesAsync(int userId)
    {
        var response = await _daySummaryRepository.GetDaySummariesByUserIdAsync(userId);
        if (response == null)
            throw new ArgumentNullException(nameof(response));
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
}
