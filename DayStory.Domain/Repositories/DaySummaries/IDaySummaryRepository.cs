using DayStory.Common.DTOs;
using DayStory.Domain.Entities;

namespace DayStory.Domain.Repositories;

public interface IDaySummaryRepository : IGenericRepository<DaySummary, DaySummaryContract>
{
    Task<List<DaySummary>> GetDaySummariesByUserIdAsync(int userId);
    Task<DaySummary> GetDaySummaryByDayAsync(string date, int userId);
    Task<List<DaySummary>> GetDaySummariesByMonthAsync(string year, string month, int userId);
    Task AddDaySummaryAsync(DaySummary daySummary);
}
