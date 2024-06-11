using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class DaySummaryRepository : GenericRepository<DaySummary, DaySummaryContract>, IDaySummaryRepository
{
    private readonly DbSet<DaySummary> _dbSet;
    private readonly DayStoryAPIDbContext _context;

    public DaySummaryRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<DaySummary>();
        _context = context;
    }

    public async Task<List<DaySummary>> GetDaySummariesByUserIdAsync(int userId)
    {
        var result = await _dbSet.Where(x => x.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        if(result != null)
            return result;
        else
            throw new DaySummaryNotFoundWithGivenUserIdException(userId.ToString());
    }

    public async Task<DaySummary> GetDaySummaryByDayAsync(string date, int userId)
    {
        var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Date == date && x.UserId == userId);

        if (result != null)
            return result;
        else
            throw new DaySummaryNotFoundWithGivenDateException(date);
    }

    public async Task<List<DaySummary>> GetDaySummariesByMonthAsync(string year, string month, int userId)
    {
        var result = await _dbSet.Where(e => e.Date.Substring(6, 4) == year && e.Date.Substring(3, 2) == month && e.UserId == userId)
            .AsNoTracking()
            .OrderBy(x => x.Date)
            .OrderBy(x => x.Id)
            .ToListAsync();

        if (result != null)
            return result;
        else
            throw new DaySummaryNotFoundWithGivenDateException(month);
    }

    public async Task AddDaySummaryAsync(DaySummary daySummary)
    {
        foreach (var eventEntity in daySummary.Events)
        {
            _context.Entry(eventEntity).State = EntityState.Unchanged;
        }

        await _dbSet.AddAsync(daySummary);
        await _context.SaveChangesAsync();
    }
}
