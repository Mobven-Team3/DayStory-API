using AutoMapper;
using DayStory.Application.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class DaySummaryService : BaseService<DaySummary, DaySummaryContract>, IDaySummaryService
{
    public DaySummaryService(IGenericRepository<DaySummary> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
