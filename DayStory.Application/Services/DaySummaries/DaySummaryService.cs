using AutoMapper;
using DayStory.Common.DTOs;
using DayStory.Application.Interfaces;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;

namespace DayStory.Application.Services;

public class DaySummaryService : BaseService<DaySummary, DaySummaryContract>, IDaySummaryService
{
    public DaySummaryService(IGenericRepository<DaySummary, DaySummaryContract> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
