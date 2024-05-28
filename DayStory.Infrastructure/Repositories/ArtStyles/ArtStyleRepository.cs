using DayStory.Common.DTOs;
using DayStory.Domain.Entities;
using DayStory.Domain.Repositories;
using DayStory.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DayStory.Infrastructure.Repositories;

internal class ArtStyleRepository : GenericRepository<ArtStyle, ArtStyleContract>, IArtStyleRepository
{
    private readonly DbSet<ArtStyle> _dbSet;

    public ArtStyleRepository(DayStoryAPIDbContext context) : base(context)
    {
        _dbSet = context.Set<ArtStyle>();
    }
}
