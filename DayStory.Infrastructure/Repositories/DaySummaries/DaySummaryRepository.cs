﻿using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Exceptions;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using DayStory.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        var result = await _dbSet.Where(x => x.UserId == userId).ToListAsync();

        return result;
    }

    public async Task<DaySummary> GetDaySummaryByDayAsync(string date, int userId)
    {
        var spec = new DaySummaryByDaySpecification(date, userId);
        var result = await FindAsync(spec);

        return result.FirstOrDefault();
    }

    public async Task<List<DaySummary>> GetDaySummariesByMonthAsync(string year, string month, int userId)
    {
        var spec = new DaySummariesByMonthSpecification(year, month, userId);
        var result = await FindAsync(spec);

        return result;
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
