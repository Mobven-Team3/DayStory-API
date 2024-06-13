using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Application.Interfaces;

public interface IDaySummaryService : IBaseService<DaySummary, DaySummaryContract>
{
    Task AddDaySummaryAsync(CreateDaySummaryContract model);
    Task<List<GetDaySummaryContract>> GetDaySummariesAsync(int userId);
    Task<GetDaySummaryContract> GetDaySummaryByIdAsync(int id, int userId);
    Task<GetDaySummaryContract> GetDaySummaryByDayAsync(GetDaySummaryByDayContract model);
    Task<List<GetDaySummaryContract>> GetDaySummariesByMonthAsync(GetDaySummariesByMonthContract model);
}
