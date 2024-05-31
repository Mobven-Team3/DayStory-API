using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class DaySummaryRepository : GenericRepository<DaySummary, DaySummaryContract>, IDaySummaryRepository
{
    private readonly DbSet<DaySummary> _dbSet;

    public DaySummaryRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<DaySummary>();
    }

    public async Task<List<DaySummary>> GetDaySummariesByUserIdAsync(int userId)
    {
        var result = await _dbSet.Where(x => x.UserId == userId).ToListAsync();
        if (result != null)
            return result;
        else
            throw new ArgumentNullException(typeof(IQueryable<DaySummary>).ToString());
    }

    public async Task<DaySummary> GetDaySummaryByDayAsync(string date, int userId)
    {
        var spec = new DaySummaryByDaySpecification(date, userId);
        var result = await FindAsync(spec);
        if (result != null)
            return result.First();
        else
            throw new ArgumentNullException(typeof(DaySummary).ToString());
    }

    public async Task<List<DaySummary>> GetDaySummariesByMonthAsync(string year, string month, int userId)
    {
        var spec = new DaySummariesByMonthSpecification(year, month, userId);
        var result = await FindAsync(spec);
        if (result != null)
            return result;
        else
            throw new ArgumentNullException(typeof(IQueryable<DaySummary>).ToString());
    }
}
