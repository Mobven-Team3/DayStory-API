using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

public class DaySummaryRepository : GenericRepository<DaySummary, DaySummaryContract>, IDaySummaryRepository
{
    private readonly DbSet<DaySummary> _dbSet;

    public DaySummaryRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<DaySummary>();
    }
}
